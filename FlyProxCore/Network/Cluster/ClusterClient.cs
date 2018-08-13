using Ether.Network.Packets;
using FlyProxCore.Config;
using FlyProxCore.Network.Packet;
using FlyProxCore.Network.Processor;
using NLog;
using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace FlyProxCore.Network.Cluster
{
    public class ClusterClient : FlyClient
    {
        protected override Logger Log => LogManager.GetCurrentClassLogger();

        protected override IPacketProcessor PacketProcessor { get; } = new FlyServerPacketProcessor<FlyServerPacket>();

        public ClusterClient()
        {
            Configuration.Host = FlyProxConfig.Instance.ClusterServerHost;
            Configuration.Port = FlyProxConfig.Instance.ClusterServerPort;
        }

        protected override void OnConnected()
        {
            Log.Debug("Proxy connected to ClusterServer.");
        }

        public override void HandleMessage(INetPacketStream packet)
        {
            base.HandleMessage(packet);

            var buffer = packet.ReadArray<byte>(packet.Size);

            var type = BitConverter.ToInt32(buffer, 0);
            if (type == 0)
                SessionId = BitConverter.ToUInt32(buffer, 4);
            
            if(type == 0x000000f2)
            {
                PatchCacheAddr(ref buffer);
            }

            using (var p = new FlyServerPacket())
            {
                p.Write(buffer, 0, buffer.Length);

                FlyProxContext.Instance.ProxyClusterServer.SendToAll(p);
            }
        }

        protected virtual void PatchCacheAddr(ref byte[] buffer)
        {
            using (var writeStream = new MemoryStream())
            using (var readStream = new MemoryStream(buffer))
            using (var reader = new BinaryReader(readStream))
            using (var writer = new BinaryWriter(writeStream))
            {
                writer.Write(reader.ReadInt32()); // PacketType

                var bHost = Encoding.ASCII.GetBytes(FlyProxConfig.Instance.ProxyServerHost);
                writer.Write(bHost.Length);
                writer.Write(bHost, 0, bHost.Length);

                buffer = writeStream.ToArray();
            }
        }

        protected override void OnDisconnected()
        {
            Log.Debug("Proxy disconnected from ClusterServer.");
            FlyProxContext.Instance.ProxyClusterServer.Clients
                .ToList()
                .ForEach(x => FlyProxContext.Instance.ProxyClusterServer.DisconnectClient(x.Id));
        }

        protected override void OnSocketError(SocketError socketError)
        {
            Log.Error($"SocketError: {Enum.GetName(typeof(SocketError), socketError)}");
        }
    }
}
using Ether.Network.Packets;
using FlyProxCore.Config;
using FlyProxCore.Network.Packet;
using FlyProxCore.Network.Processor;
using NLog;
using System;
using System.Net.Sockets;

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
            
            using (var p = new FlyServerPacket())
            {
                p.Write(buffer, 0, buffer.Length);

                FlyProxContext.Instance.ClusterProxyServer.SendToAll(p);
            }
        }

        protected override void OnDisconnected()
        {
            Log.Debug("Proxy disconnected from ClusterServer.");
        }

        protected override void OnSocketError(SocketError socketError)
        {
            Log.Error($"SocketError: {Enum.GetName(typeof(SocketError), socketError)}");
        }
    }
}
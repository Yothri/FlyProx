using Ether.Network.Packets;
using FlyProxCore.Config;
using FlyProxCore.Network.Packet;
using NLog;
using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace FlyProxCore.Network.Login
{
    public class LoginClient : FlyClient
    {
        protected override Logger Log => LogManager.GetCurrentClassLogger();
        
        public LoginClient()
        {
            Configuration.Host = FlyProxConfig.Instance.LoginServerHost;
            Configuration.Port = FlyProxConfig.Instance.LoginServerPort;
        }

        protected override void OnConnected()
        {
            Log.Debug("Proxy connected to LoginServer.");
        }

        public override void HandleMessage(INetPacketStream packet)
        {
            base.HandleMessage(packet);

            var buffer = packet.ReadArray<byte>(packet.Size);

            var type = BitConverter.ToInt32(buffer, 0);
            if (type == 0)
                SessionId = BitConverter.ToUInt32(buffer, 4);

            if(type == 0x000000fd)
            {
                PatchServerList(ref buffer);
            }

            using (var p = new FlyServerPacket())
            {
                p.Write(buffer, 0, buffer.Length);

                FlyProxContext.Instance.ProxyLoginServer.SendToAll(p);
            }
        }

        protected virtual void PatchServerList(ref byte[] buffer)
        {
            using (var writeStream = new MemoryStream())
            using (var readStream = new MemoryStream(buffer))
            using (var reader = new BinaryReader(readStream))
            using (var writer = new BinaryWriter(writeStream))
            {
                writer.Write(reader.ReadInt32()); // PacketType

                writer.Write(reader.ReadInt32());
                writer.Write(reader.ReadByte());

                var usernameLen = reader.ReadInt32();
                writer.Write(usernameLen);
                writer.Write(reader.ReadBytes(usernameLen));
                writer.Write(2); // 1 Server, 1 Channel

                #region Cluster
                writer.Write(-1);
                writer.Write(1); // Cluster Id

                // Cluster Name
                var bClusterName = Encoding.ASCII.GetBytes("FlyProx-Server");
                writer.Write(bClusterName.Length);
                writer.Write(bClusterName);

                // Cluster Host
                var bClusterHost = Encoding.ASCII.GetBytes(FlyProxConfig.Instance.ProxyServerHost);
                writer.Write(bClusterHost.Length);
                writer.Write(bClusterHost);

                writer.Write(0);
                writer.Write(0);
                writer.Write(1);
                writer.Write(0);
                #endregion

                #region World
                writer.Write(1); // Cluster Id
                writer.Write(1); // World Id

                // World Name
                var bWorldName = Encoding.ASCII.GetBytes("FlyProx-Channel");
                writer.Write(bWorldName.Length);
                writer.Write(bWorldName);

                // World Host
                writer.Write(bClusterHost.Length);
                writer.Write(bClusterHost);

                writer.Write(0);
                writer.Write(0);
                writer.Write(1);
                writer.Write(100); // Capacity
                #endregion

                buffer = writeStream.ToArray();
            }
        }

        protected override void OnDisconnected()
        {
            Log.Debug("Proxy disconnected from LoginServer.");
            FlyProxContext.Instance.ProxyLoginServer.Clients
                .ToList()
                .ForEach(x => FlyProxContext.Instance.ProxyLoginServer.DisconnectClient(x.Id));
        }

        protected override void OnSocketError(SocketError socketError)
        {
            Log.Error($"SocketError: {Enum.GetName(typeof(SocketError), socketError)}");
        }
    }
}
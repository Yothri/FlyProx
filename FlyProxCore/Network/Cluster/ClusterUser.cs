using Ether.Network.Packets;
using FlyProxCore.Network.Packet;
using System.Linq;

namespace FlyProxCore.Network.Cluster
{
    public class ClusterUser : FlyUser
    {
        public override FlyClient Client { get; } = new ClusterClient();

        public override void HandleMessage(INetPacketStream packet)
        {
            base.HandleMessage(packet);

            var buffer = packet.ReadArray<byte>(packet.Size).Skip(4).ToArray();

            using (var p = new FlyClientPacket(Client.SessionId))
            {
                p.Write(buffer, 0, buffer.Length);

                Client.Send(p);
            }
        }
    }
}
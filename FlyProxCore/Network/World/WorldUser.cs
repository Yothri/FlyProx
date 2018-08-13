using Ether.Network.Packets;
using FlyProxCore.Network.Packet;
using System.Linq;

namespace FlyProxCore.Network.World
{
    public class WorldUser : FlyUser
    {
        public override void HandleMessage(INetPacketStream packet)
        {
            base.HandleMessage(packet);

            var buffer = packet.ReadArray<byte>(packet.Size).Skip(4).ToArray();

            using (var p = new FlyClientPacket(FlyProxContext.Instance.ProxyWorldClient.SessionId))
            {
                p.Write(buffer, 0, buffer.Length);

                FlyProxContext.Instance.ProxyWorldClient.Send(p);
            }
        }
    }
}
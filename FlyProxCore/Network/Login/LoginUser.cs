using Ether.Network.Packets;
using FlyProxCore.Network.Packet;

namespace FlyProxCore.Network.Login
{
    public class LoginUser : FlyUser
    {
        public override void HandleMessage(INetPacketStream packet)
        {
            base.HandleMessage(packet);

            var buffer = packet.ReadArray<byte>(packet.Size);

            using (var p = new FlyClientPacket(FlyProxContext.Instance.ProxyLoginClient.SessionId, false))
            {
                p.Write(buffer, 0, buffer.Length);

                FlyProxContext.Instance.ProxyLoginClient.Send(p);
            }
        }
    }
}
using Ether.Network.Packets;
using FlyProxCore.Network.Packet;

namespace FlyProxCore.Network.Login
{
    public class LoginUser : FlyUser
    {
        public override FlyClient Client { get; } = new LoginClient();

        public override void HandleMessage(INetPacketStream packet)
        {
            base.HandleMessage(packet);

            var buffer = packet.ReadArray<byte>(packet.Size);

            using (var p = new FlyClientLoginPacket(Client.SessionId))
            {
                p.Write(buffer, 0, buffer.Length);

                Client.Send(p);
            }
        }
    }
}
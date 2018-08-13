using Ether.Network.Packets;
using FlyProxCore.Network.Login;
using FlyProxCore.Network.Processor;
using FlyProxInsanity.Network.Packet;
using NLog;

namespace FlyProxInsanity.Network.Login
{
    public class InsanityLoginServer : LoginServer
    {
        protected override Logger Log => LogManager.GetCurrentClassLogger();

        protected override IPacketProcessor PacketProcessor { get; } = new FlyClientPacketProcessor<InsanityClientLoginPacket>();
    }
}
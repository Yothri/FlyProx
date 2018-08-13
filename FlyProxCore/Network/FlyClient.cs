using Ether.Network.Client;
using Ether.Network.Packets;
using FlyProxCore.Network.Packet;
using FlyProxCore.Network.Processor;
using NLog;

namespace FlyProxCore.Network
{
    public abstract class FlyClient : NetClient
    {
        protected virtual Logger Log => LogManager.GetCurrentClassLogger();

        protected override IPacketProcessor PacketProcessor { get; } = new FlyServerPacketProcessor<FlyServerPacket>();

        public uint SessionId { get; protected set; }
    }
}
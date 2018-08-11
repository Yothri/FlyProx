using Ether.Network.Packets;
using FlyProxCore.Config;

namespace FlyProxCore.Network.Packet
{
    public abstract class FlyPacket : NetPacketStream
    {
        public virtual byte HeaderMark { get; } = (byte)FlyProxConfig.Instance.HeaderMark;

        public FlyPacket()
        {
        }

        public FlyPacket(byte[] buffer)
            : base(buffer)
        {
        }
    }
}
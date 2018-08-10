using Ether.Network.Packets;

namespace FlyProxCore.Network.Packet
{
    public abstract class FlyPacket : NetPacketStream
    {
        public virtual byte HeaderMark { get; } = (byte)'^';

        public FlyPacket()
        {
        }

        public FlyPacket(byte[] buffer)
            : base(buffer)
        {
        }
    }
}
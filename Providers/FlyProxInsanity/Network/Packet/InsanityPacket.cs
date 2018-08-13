using FlyProxCore.Network.Packet;

namespace FlyProxInsanity.Network.Packet
{
    public abstract class InsanityPacket : FlyPacket
    {
        public override byte HeaderMark => (byte)'!';

        public InsanityPacket()
        {
        }

        public InsanityPacket(byte[] buffer)
            : base(buffer)
        {
        }
    }
}
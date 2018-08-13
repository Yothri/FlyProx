using FlyProxCore.Network.Packet;

namespace FlyProxInsanity.Network.Packet
{
    public class InsanityClientLoginPacket : FlyClientLoginPacket
    {
        public override byte HeaderMark => (byte)'!';
    }
}
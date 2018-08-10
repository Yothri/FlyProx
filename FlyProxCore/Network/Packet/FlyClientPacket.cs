namespace FlyProxCore.Network.Packet
{
    public class FlyClientPacket : FlyClientLoginPacket
    {
        public FlyClientPacket(uint sessionId)
            : base(sessionId)
        {
            Write(0xFFFFFFFF);
        }

        public FlyClientPacket(byte[] buffer)
            : base(buffer)
        {
        }
    }
}
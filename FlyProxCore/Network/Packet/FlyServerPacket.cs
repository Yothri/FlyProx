using System.IO;

namespace FlyProxCore.Network.Packet
{
    public class FlyServerPacket : FlyPacket
    {

        public override byte[] Buffer => BuildBuffer();

        public FlyServerPacket()
        {
            Write(HeaderMark);
            Write(0);
        }

        public FlyServerPacket(byte[] buffer)
            : base(buffer)
        {
        }

        protected virtual byte[] BuildBuffer()
        {
            var len = (uint)(Length - 5);
            var oldPointer = Position;

            Seek(1, SeekOrigin.Begin);
            Write(len);
            Seek(oldPointer, SeekOrigin.Begin);

            return base.Buffer;
        }
    }
}
using FlyProxCore.Config;
using FlyProxCore.Cryptography;
using System;
using System.IO;
using System.Linq;

namespace FlyProxCore.Network.Packet
{
    public class FlyClientLoginPacket : FlyPacket
    {
        public uint SessionId { get; }
        public override byte[] Buffer => BuildBuffer();

        public FlyClientLoginPacket(uint sessionId)
            : this()
        {
            SessionId = sessionId;
        }

        protected FlyClientLoginPacket()
        {
            Write(HeaderMark);
            Write(0);
            Write(0);
            Write(0);
        }

        public FlyClientLoginPacket(byte[] buffer)
            : base(buffer)
        {
        }

        protected virtual byte[] BuildBuffer()
        {
            var pointer = Position;

            var len = (uint)(Length - 13);
            var bLen = BitConverter.GetBytes(len);
            var lenHash = ~(Crc32.ComputeChecksum(bLen, FlyProxConfig.Instance.CRCKey) ^ SessionId);

            var data = base.Buffer.Skip(13).ToArray();
            var dataHash = ~(Crc32.ComputeChecksum(data, FlyProxConfig.Instance.CRCKey) ^ SessionId);

            Seek(1, SeekOrigin.Begin);
            Write(lenHash);
            Write(len);
            Write(dataHash);
            Seek(pointer, SeekOrigin.Begin);

            return base.Buffer;
        }
    }
}
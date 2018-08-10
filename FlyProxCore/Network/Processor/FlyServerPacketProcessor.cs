using Ether.Network.Packets;
using FlyProxCore.Network.Packet;
using System;

namespace FlyProxCore.Network.Processor
{
    public class FlyServerPacketProcessor<TPacket> : IPacketProcessor
        where TPacket : FlyPacket
    {
        public bool IncludeHeader => false;

        public int HeaderSize => 5;

        public int GetMessageLength(byte[] buffer)
        {
            var packet = Activator.CreateInstance(typeof(TPacket), buffer) as TPacket;
            if (packet.ReadByte() != packet.HeaderMark)
                return 0;

            return packet.Read<int>();
        }

        public INetPacketStream CreatePacket(byte[] buffer)
        {
            return Activator.CreateInstance(typeof(TPacket), buffer) as TPacket;
        }
    }
}
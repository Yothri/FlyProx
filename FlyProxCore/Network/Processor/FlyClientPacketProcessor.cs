using Ether.Network.Packets;
using FlyProxCore.Network.Packet;
using System;

namespace FlyProxCore.Network.Processor
{
    public class FlyClientPacketProcessor<TPacket> : IPacketProcessor
        where TPacket : FlyPacket
    {
        public int HeaderSize => 13;

        public bool IncludeHeader => false;

        public int GetMessageLength(byte[] buffer)
        {
            var packet = Activator.CreateInstance(typeof(TPacket), buffer) as TPacket;
            if (packet.ReadByte() != packet.HeaderMark)
                return 0;

            packet.Read<uint>(); // Length Hash

            return packet.Read<int>();
        }

        public INetPacketStream CreatePacket(byte[] buffer)
        {
            return Activator.CreateInstance(typeof(TPacket), buffer) as TPacket;
        }
    }
}
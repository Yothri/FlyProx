using Ether.Network.Packets;
using Ether.Network.Server;
using NLog;

namespace FlyProxCore.Network
{
    public abstract class FlyServer<TUser> : NetServer<TUser>
        where TUser : FlyUser, new()
    {
        protected virtual Logger Log => LogManager.GetCurrentClassLogger();

        protected override IPacketProcessor PacketProcessor => null;
    }
}
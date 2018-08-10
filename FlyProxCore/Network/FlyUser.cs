using Ether.Network.Common;
using NLog;

namespace FlyProxCore.Network
{
    public abstract class FlyUser : NetUser
    {
        protected virtual Logger Log => LogManager.GetCurrentClassLogger();

        public abstract FlyClient Client { get; }
    }
}
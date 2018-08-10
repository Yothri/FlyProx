using Ether.Network.Client;
using NLog;

namespace FlyProxCore.Network
{
    public abstract class FlyClient : NetClient
    {
        protected virtual Logger Log => LogManager.GetCurrentClassLogger();

        public uint SessionId { get; protected set; }
    }
}
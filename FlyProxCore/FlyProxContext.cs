using FlyProxCore.Config;
using FlyProxCore.Network.Cluster;
using FlyProxCore.Network.Login;
using FlyProxCore.Network.World;

namespace FlyProxCore
{
    public class FlyProxContext : IFlyProxContext
    {
        public static FlyProxContext Instance;

        public virtual string Name => "Default";
        public virtual LoginServer ProxyLoginServer { get; } = new LoginServer();
        public virtual ClusterServer ProxyClusterServer { get; } = new ClusterServer();
        public virtual WorldServer ProxyWorldServer { get; } = new WorldServer();

        public virtual LoginClient ProxyLoginClient { get; } = new LoginClient();
        public virtual ClusterClient ProxyClusterClient { get; } = new ClusterClient();
        public virtual WorldClient ProxyWorldClient { get; } = new WorldClient();

        public FlyProxContext()
        {
            FlyProxConfig.Instance.Load();
            Instance = this;
        }

        public void Initialize()
        {
            ProxyLoginServer.Start();
            ProxyClusterServer.Start();
            ProxyWorldServer.Start();
        }

        public void Uninitialize()
        {
            ProxyLoginServer.Stop();
            ProxyClusterServer.Stop();
            ProxyWorldServer.Stop();

            ProxyLoginClient.Disconnect();
            ProxyClusterClient.Disconnect();
            ProxyWorldClient.Disconnect();
        }

        public void Dispose()
        {
            FlyProxConfig.Instance.Dispose();
        }
    }
}
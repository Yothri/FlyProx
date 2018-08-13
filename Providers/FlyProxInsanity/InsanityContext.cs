using FlyProxCore;
using FlyProxCore.Network.Cluster;
using FlyProxCore.Network.Login;
using FlyProxCore.Network.World;

namespace FlyProxInsanity
{
    public class InsanityContext : FlyProxContext
    {
        public override string Name => "Insanity FlyFF";

        public override LoginServer ProxyLoginServer => base.ProxyLoginServer;

        public override ClusterServer ProxyClusterServer => base.ProxyClusterServer;

        public override WorldServer ProxyWorldServer => base.ProxyWorldServer;

        public override LoginClient ProxyLoginClient => base.ProxyLoginClient;

        public override ClusterClient ProxyClusterClient => base.ProxyClusterClient;

        public override WorldClient ProxyWorldClient => base.ProxyWorldClient;
    }
}
using FlyProxCore.Network.Cluster;
using FlyProxCore.Network.Login;
using FlyProxCore.Network.World;
using System;

namespace FlyProxCore
{
    public interface IFlyProxContext : IDisposable
    {
        string Name { get; }
        
        LoginServer ProxyLoginServer { get; }
        ClusterServer ProxyClusterServer { get; }
        WorldServer ProxyWorldServer { get; }

        LoginClient ProxyLoginClient { get; }
        ClusterClient ProxyClusterClient { get; }
        WorldClient ProxyWorldClient { get; }
    }
}
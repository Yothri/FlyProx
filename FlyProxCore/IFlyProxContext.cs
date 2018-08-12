using FlyProxCore.Network.Cluster;
using FlyProxCore.Network.Login;
using FlyProxCore.Network.World;
using System;

namespace FlyProxCore
{
    public interface IFlyProxContext : IDisposable
    {
        string Name { get; }
        LoginServer LoginProxyServer { get; }
        ClusterServer ClusterProxyServer { get; }
        WorldServer WorldProxyServer { get; }
    }
}
using FlyProxCore.Config;
using FlyProxCore.Network.Cluster;
using FlyProxCore.Network.Login;
using FlyProxCore.Network.World;
using System;

namespace FlyProxCore
{
    public class FlyProxContext : IFlyProxContext
    {
        public static FlyProxContext Instance;

        public virtual string Name => "Default";
        public virtual LoginServer LoginProxyServer { get; } = new LoginServer();
        public virtual ClusterServer ClusterProxyServer { get; } = new ClusterServer();
        public virtual WorldServer WorldProxyServer { get; } = new WorldServer();

        public FlyProxContext()
        {
            FlyProxConfig.Instance.Load();
            Instance = this;
        }

        public void Initialize()
        {
            LoginProxyServer.Start();
            ClusterProxyServer.Start();
            WorldProxyServer.Start();
        }

        public void Uninitialize()
        {
            LoginProxyServer.Stop();
            ClusterProxyServer.Stop();
            WorldProxyServer.Stop();
        }

        public void Dispose()
        {
            LoginProxyServer.Dispose();
            ClusterProxyServer.Dispose();
            WorldProxyServer.Dispose();
            FlyProxConfig.Instance.Dispose();
        }
    }
}
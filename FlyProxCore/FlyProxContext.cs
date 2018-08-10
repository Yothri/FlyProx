using FlyProxCore.Config;
using FlyProxCore.Network.Cluster;
using FlyProxCore.Network.Login;
using System;

namespace FlyProxCore
{
    public class FlyProxContext : IDisposable
    {
        public static FlyProxContext Instance;

        public virtual LoginServer LoginProxyServer { get; } = new LoginServer();
        public virtual ClusterServer ClusterProxyServer { get; } = new ClusterServer();

        public FlyProxContext()
        {
            Instance = this;
        }

        public void Initialize()
        {
            LoginProxyServer.Start();
            ClusterProxyServer.Start();
        }

        public void Uninitialize()
        {
            LoginProxyServer.Stop();
            ClusterProxyServer.Stop();
        }

        public void Dispose()
        {
            LoginProxyServer.Dispose();
            ClusterProxyServer.Dispose();
            FlyProxConfig.Instance.Dispose();
        }
    }
}
using Ether.Network.Packets;
using FlyProxCore.Config;
using FlyProxCore.Network.Packet;
using FlyProxCore.Network.Processor;
using NLog;
using System;

namespace FlyProxCore.Network.Cluster
{
    public class ClusterServer : FlyServer<ClusterUser>
    {
        protected override Logger Log => LogManager.GetCurrentClassLogger();

        protected override IPacketProcessor PacketProcessor { get; } = new FlyClientPacketProcessor<FlyClientPacket>();

        public ClusterServer()
        {
            Configuration.Host = FlyProxConfig.Instance.ProxyServerHost;
            Configuration.Port = FlyProxConfig.Instance.ProxyClusterServerPort;
            Configuration.Blocking = false;
        }

        protected override void Initialize()
        {
            Log.Debug("Initialized!");
        }

        protected override void OnClientConnected(ClusterUser connection)
        {
            Log.Debug("Game connected to ClusterProxyServer!");
            connection.Client.Connect();
        }

        protected override void OnClientDisconnected(ClusterUser connection)
        {
            Log.Debug("Game disconnected from ClusterProxyServer!");
            connection.Client.Disconnect();
        }

        protected override void OnError(Exception exception)
        {
            Log.Error($"Error: {exception.ToString()}");
        }
    }
}
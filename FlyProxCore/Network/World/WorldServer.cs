using Ether.Network.Packets;
using FlyProxCore.Config;
using FlyProxCore.Network.Packet;
using FlyProxCore.Network.Processor;
using NLog;
using System;

namespace FlyProxCore.Network.World
{
    public class WorldServer : FlyServer<WorldUser>
    {
        protected override Logger Log => LogManager.GetCurrentClassLogger();

        protected override IPacketProcessor PacketProcessor { get; } = new FlyClientPacketProcessor<FlyClientPacket>();

        public WorldServer()
        {
            Configuration.Host = FlyProxConfig.Instance.ProxyServerHost;
            Configuration.Port = FlyProxConfig.Instance.ProxyWorldServerPort;
            Configuration.Blocking = false;
        }

        protected override void Initialize()
        {
            Log.Debug("Initialized!");
        }

        protected override void OnClientConnected(WorldUser connection)
        {
            Log.Debug("Game connected to WorldProxyServer!");
            FlyProxContext.Instance.ProxyWorldClient.Connect();
        }

        protected override void OnClientDisconnected(WorldUser connection)
        {
            Log.Debug("Game disconnected from WorldProxyServer!");
            FlyProxContext.Instance.ProxyWorldClient.Disconnect();
        }

        protected override void OnError(Exception exception)
        {
            Log.Error($"Error: {exception.ToString()}");
        }
    }
}
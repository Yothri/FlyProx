using Ether.Network.Packets;
using FlyProxCore.Config;
using FlyProxCore.Network.Packet;
using FlyProxCore.Network.Processor;
using NLog;
using System;

namespace FlyProxCore.Network.Login
{
    public class LoginServer : FlyServer<LoginUser>
    {
        protected override Logger Log => LogManager.GetCurrentClassLogger();

        protected override IPacketProcessor PacketProcessor { get; } = new FlyClientPacketProcessor<FlyClientLoginPacket>();

        public LoginServer()
        {
            Configuration.Host = FlyProxConfig.Instance.ProxyServerHost;
            Configuration.Port = FlyProxConfig.Instance.ProxyLoginServerPort;
            Configuration.Blocking = false;
        }

        protected override void Initialize()
        {
            Log.Debug("Initialized!");
        }

        protected override void OnClientConnected(LoginUser connection)
        {
            Log.Debug("Game connected to LoginProxyServer!");
            FlyProxContext.Instance.ProxyLoginClient.Connect();
        }

        protected override void OnClientDisconnected(LoginUser connection)
        {
            Log.Debug("Game disconnected from LoginProxyServer!");
            FlyProxContext.Instance.ProxyLoginClient.Disconnect();
        }

        protected override void OnError(Exception exception)
        {
            Log.Error($"Error: {exception.ToString()}");
        }
    }
}
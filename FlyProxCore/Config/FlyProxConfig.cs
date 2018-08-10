namespace FlyProxCore.Config
{
    public class FlyProxConfig : Configuration<FlyProxConfig>
    {
        public string ProxyServerHost { get; set; }

        public int ProxyLoginServerPort { get; set; }
        public int ProxyClusterServerPort { get; set; }
        public int ProxyWorldServerPort { get; set; }

        public string LoginServerHost { get; set; }
        public int LoginServerPort { get; set; }

        public string ClusterServerHost { get; set; }
        public int ClusterServerPort { get; set; }

        public string WorldServerHost { get; set; }
        public int WorldServerPort { get; set; }

        public FlyProxConfig()
        {
            ProxyServerHost = "127.0.0.1";

            ProxyLoginServerPort = 23000;
            ProxyClusterServerPort = 28000;
            ProxyWorldServerPort = 5400;

            LoginServerHost = "85.214.251.19";
            LoginServerPort = 23000;

            ClusterServerHost = "85.214.251.19";
            ClusterServerPort = 28000;

            WorldServerHost = "85.214.251.19";
            WorldServerPort = 5400;
        }
    }
}
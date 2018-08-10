using Newtonsoft.Json;
using System;

namespace FlyProxCore.Config
{
    public interface IConfiguration : IDisposable
    {
        [JsonIgnore]
        string ConfigFile { get; }

        void Load();

        void Save();
    }
}
using Newtonsoft.Json;
using System;
using System.IO;

namespace FlyProxCore.Config
{
    public abstract class Configuration<TConfig> : IConfiguration
        where TConfig : IConfiguration, new()
    {
        private static TConfig instance;
        public static TConfig Instance
        {
            get
            {
                return instance == null ? (instance = new TConfig()) : instance;
            }
        }

        public virtual string ConfigFile => "config.json";

        public Configuration()
        {
            Load();
        }

        public virtual void Load()
        {
            if (!File.Exists(ConfigFile))
                Save();

            JsonConvert.PopulateObject(File.ReadAllText(ConfigFile), this);
        }

        public virtual void Save()
        {
            if (Path.GetDirectoryName(ConfigFile) != string.Empty && !Directory.Exists(Path.GetDirectoryName(ConfigFile)))
                Directory.CreateDirectory(Path.GetDirectoryName(ConfigFile));

            File.WriteAllText(ConfigFile, JsonConvert.SerializeObject(this, Formatting.Indented));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                Save();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
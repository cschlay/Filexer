using System;
using Filexer.Features;
using Filexer.Features.Indexer;

namespace Filexer.Utilities
{
    /// <summary>Use this class to initialize configuration</summary>
    public static class Configuration
    {
        private const string DefaultConfigFileName = ".filexer.json";
        private const string EnvKeyConfigFilePath = "FILEXER_CONFIG_FILE";
        
        /// <summary>
        /// Creates the configuration by reading configuration file from home directory if it exists.
        /// </summary>
        public static FileSystemOptions ReadOptions()
        {
            string userHome = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string? configFilePath = Environment.GetEnvironmentVariable(EnvKeyConfigFilePath);
            if (configFilePath == null)
            {
                configFilePath = $"{userHome}/{DefaultConfigFileName}";
            }
            return JsonUtilities.ReadFromJson<FileSystemOptions>(configFilePath);
        }
    }
}

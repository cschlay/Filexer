using System;
using System.IO;

namespace Filexer.Features.Indexer
{
    public class OutputDirectories
    {
        public readonly string BasePath;
        public string DevDirectory => $"{BasePath}/dev";
        public string DocumentDirectory => $"{BasePath}/documents";
        public string ProjectDirectory => $"{BasePath}/projects";
        public string SecretsDirectory => $"{BasePath}/secrets";
        public string OfficeDirectory => $"{BasePath}/office";
        public string MiscDirectory => $"{BasePath}/misc";
        
        public OutputDirectories(string basePath)
        {
            BasePath = basePath;
        }

        public DirectoryInfo Initialize()
        {
            Console.WriteLine("Initializing sync directories...");
            DirectoryInfo workingDirectory = Directory.CreateDirectory(BasePath);

            var directories = new[]
            {
                DevDirectory,
                $"{DevDirectory}/.ssh",
                DocumentDirectory,
                OfficeDirectory,
                ProjectDirectory,
                MiscDirectory
            };
            foreach (string directory in directories)
            {
                Directory.CreateDirectory(directory);
            }
            
            return workingDirectory;
        }
        
        public void Reset()
        {
            try
            {
                Directory.Delete(BasePath, true);
            }
            catch (DirectoryNotFoundException) { }
        }
    }
}
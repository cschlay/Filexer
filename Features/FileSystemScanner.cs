using System;
using System.IO;

namespace Filexer.Features
{
    /// <summary>
    /// Contains utilities to scan the file system.
    /// </summary>
    public class FileSystemScanner
    {
        private readonly FileSystemOptions _options;
        
        /// <summary>
        /// Prepares scanning by settings.
        /// </summary>
        /// <param name="options">Platform specific options for scanning</param>
        public FileSystemScanner(FileSystemOptions options)
        {
            _options = options;
        }
        
        /// <summary>Scans the system by most probable paths and user directories.</summary>
        public void Index()
        {
            Console.WriteLine("Starting to index...");
            Console.WriteLine($"Checking home directory: {_options.UserHomePath}");

            string[] directories = Directory.GetDirectories(_options.UserHomePath);

            foreach (string directory in directories)
            {
                var info = new DirectoryInfo(directory);
                bool isHidden = (info.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden;
                if (isHidden)
                {
                    Console.WriteLine(directory);
                }
                else
                {
                    // TODO: Directories like .ssh need to indexed.
                }
            }
        }

        private void Index(string path)
        {
            
        }
    }
}
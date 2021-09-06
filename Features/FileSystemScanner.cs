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
        private DirectoryInfo _workingDirectory;
        private GitService _git;
        
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
            try
            {
                Directory.Delete(_options.SyncDirectory, true);
            }
            catch (DirectoryNotFoundException)
            {
            }
            _workingDirectory = Directory.CreateDirectory(_options.SyncDirectory);
            _git = new GitService(_workingDirectory);
            Console.WriteLine("Initializing sync directories...");
            Directory.CreateDirectory(_options.DevDirectory);
            Directory.CreateDirectory(_options.DocumentDirectory);
            Directory.CreateDirectory(_options.ProjectDirectory);
            Directory.CreateDirectory(_options.OfficeDirectory);
            Directory.CreateDirectory(_options.MiscDirectory);
         
            Console.WriteLine("Starting to index...");
            IndexDirectory(_options.UserHomePath, 0);
            Console.WriteLine($"Indexing finished.");
            Console.WriteLine($"Result stored at: {_workingDirectory.FullName}");
        }

        private bool IndexFile(string path, int depth)
        {
            if (_options.CheckFileIgnore(path))
            {
                return false;
            }
            
            Console.WriteLine($"{depth} File: {path}");
            // TODO: Index the files.
            return true;
        }
        
        private bool IndexDirectory(string path, int depth)
        {
            var info = new DirectoryInfo(path);

            if (_options.CheckDirectoryIgnore(info))
            {
                return false;
            }
            
            Console.WriteLine($"{depth} Directory: {info.Name} ({path})");
            string[] directories = Directory.GetDirectories(path);
            if (ContainsRepository(directories))
            {
                Console.WriteLine($"Git repository found: {info.Name}");
                _git.Clone(info);
                // TODO: Check last pull
                // TODO: Check last push
                // TODO: Check that it is up to date
                return true;
            }
            else
            {
                // Continue indexing normally
                foreach (string directory in directories)
                {
                    IndexDirectory(directory, depth + 1);
                }
            }

            string[] files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                IndexFile(file, depth);
            }
            return true;
        }

        private bool ContainsRepository(string[] directories)
        {
            // TODO: Inefficient implementation
            foreach (string directory in directories)
            {
                // It cannot distinguish files from directories
                if (Path.GetFileName(directory) == ".git")
                {
                    return true;
                }
            }

            return false;
        }
    }
}

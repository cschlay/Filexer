using System;
using System.IO;
using System.IO.Compression;

namespace Filexer.Features
{
    /// <summary>
    /// Contains utilities to scan the file system.
    /// </summary>
    public class FileSystemScanner
    {
        // TODO: Create JSON Source maps
        private readonly FileSystemOptions _options;
        private readonly DirectoryInfo _workingDirectory;
        private readonly GitService _git;
        
        /// <summary>
        /// Prepares scanning by settings.
        /// </summary>
        /// <param name="options">Platform specific options for scanning</param>
        public FileSystemScanner(FileSystemOptions options)
        {
            _options = options;
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
            Directory.CreateDirectory($"{_options.DevDirectory}/.ssh");
            Directory.CreateDirectory(_options.DocumentDirectory);
            Directory.CreateDirectory(_options.ProjectDirectory);
            Directory.CreateDirectory(_options.OfficeDirectory);
            Directory.CreateDirectory(_options.MiscDirectory);
        }
        
        /// <summary>Scans the system by most probable paths and user directories.</summary>
        public void Index()
        {
            Console.WriteLine("Starting to index...");
            IndexDirectory(_options.UserHomePath, 0);
            Console.WriteLine($"Indexing finished.");
            Console.WriteLine("Creating archive from collected files...");

            string timestamp = DateTime.UtcNow.ToString("s").Replace(":", "");
            string archiveName = $"{_workingDirectory.Parent!.FullName}/backup_{timestamp}.zip";
            ZipFile.CreateFromDirectory(_workingDirectory.FullName, archiveName);
            Console.WriteLine($"Result stored at: {_workingDirectory.FullName}");
        }

        private bool IndexFile(string source, int depth)
        {
            if (_options.CheckFileIgnore(source))
            {
                return false;
            }
            Console.WriteLine($"{depth} File: {source}");

            var info = new FileInfo(source);
            string timestamp = info.LastWriteTimeUtc.ToString("s").Replace(":", "");
            if (info.Extension == ".pdf")
            {
                File.Copy(source, $"{_options.DocumentDirectory}/{timestamp}_{info.Name}");
            } else if (info.FullName.Contains(".ssh"))
            {
                File.Copy(source, $"{_options.DevDirectory}/.ssh/{timestamp}_{info.Name}");
            }
            else if (info.Extension == ".pem")
            {
                File.Copy(source, $"{_options.SecretsDirectory}/{timestamp}_{info.Name}");
            }
            else if (Array.IndexOf(FileSystemOptions.officeExtensions, info.Extension) > -1)
            {
                File.Copy(source, $"{_options.OfficeDirectory}/{timestamp}_{info.Name}");
            }
            else
            {
                File.Copy(source, $"{_options.MiscDirectory}/{timestamp}_{info.Name}");
            }
            
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

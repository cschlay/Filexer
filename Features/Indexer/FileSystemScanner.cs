using System;
using System.IO;
using System.IO.Compression;
using Filexer.Utilities;

namespace Filexer.Features.Indexer
{
    /// <summary>
    /// Contains utilities to scan the file system.
    /// </summary>
    public class FileSystemScanner
    {
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
            _options.Output.Reset();
            _workingDirectory = _options.Output.Initialize();
            _git = new GitService(_workingDirectory);
        }
        
        /// <summary>Scans the system by most probable paths and user directories.</summary>
        public void Index()
        {
            Console.WriteLine("Starting to index...");
            foreach (string rootDirectory in _options.IncludedRootDirectories)
            {
                IndexDirectory(rootDirectory, 0);
            }
            
            Console.WriteLine($"Indexing finished.");
            Console.WriteLine("Creating archive from collected files...");

            string timestamp = DateTime.UtcNow.ToFileNameTime();
            string archiveName = $"{_workingDirectory.Parent!.FullName}/backup_{timestamp}.zip";
            ZipFile.CreateFromDirectory(_workingDirectory.FullName, archiveName);
            Console.WriteLine($"Result stored at: {_workingDirectory.FullName}");
        }

        /// <summary>
        /// Makes a synchronization copy of the file if meets the criteria.
        /// </summary>
        /// <param name="source">path of the file</param>
        /// <returns>true if file is copied</returns>
        private bool IndexFile(string source)
        {
            FileCategory? category = _options.Extensions.GetFileCategory(source);
            if (category == null)
            {
                return false;
            }
            var info = new FileInfo(source);
            string timestamp = info.LastWriteTimeUtc.ToFileNameTime();
            string fileName = $"{timestamp}_{info.Name}";
            string outputFile = _options.Output.GetOutputDirectory((FileCategory)category) + fileName;
            
            File.Copy(source, outputFile);
            return true;
        }
        
        private bool IndexDirectory(string path, int depth)
        {
            var info = new DirectoryInfo(path);

            if (_options.IsDirectoryIgnored(info))
            {
                return false;
            }
            
            Console.WriteLine($"{depth} Directory: {info.Name} ({path})");
            string[] directories = Directory.GetDirectories(path);
            if (_git.ContainsRepository(directories))
            {
                Console.WriteLine($"Git repository found: {info.Name}");
                if (!_options.CloneGitRepositories)
                {
                    return false;
                }
                
                _git.Clone(info);
                return true;
            }

            // Continue indexing normally
            foreach (string directory in directories)
            {
                IndexDirectory(directory, depth + 1);
            }

            string[] files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                IndexFile(file);
            }
            return true;
        }
    }
}

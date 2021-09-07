using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Filexer.Utilities;

namespace Filexer.Features
{
    /// <summary>Call git methods and inspect the repository.</summary>
    public class GitService
    {
        private readonly DirectoryInfo _workingDirectory;
        private const string GitFolderName = ".git";

        public GitService(DirectoryInfo workingDirectory)
        {
            _workingDirectory = workingDirectory;
        }
        
        public void Clone(DirectoryInfo source)
        {
            Console.WriteLine("Cloning repository...");
            string timestamp = source.LastWriteTimeUtc.ToFileNameTime();
            string target = $"{source.Name}_{timestamp}";
            var info = new ProcessStartInfo
            {
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                WorkingDirectory = $"{_workingDirectory.FullName}/projects",
                FileName = "git",
                Arguments = $"clone {source.FullName} {target}"
            };

            var process = new Process
            {
                StartInfo = info
            };
            process.Start();
            process.WaitForExit();
            process.Close();
        }
        
        public bool ContainsRepository(IEnumerable<string> directories)
        {
            return directories.Any(directory => Path.GetFileName(directory) == GitFolderName);
        }
    }
}
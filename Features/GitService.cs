using System;
using System.Diagnostics;
using System.IO;

namespace Filexer.Features
{
    /// <summary>Call git methods and inspect the repository.</summary>
    public class GitService
    {
        private readonly DirectoryInfo _workingDirectory;

        public GitService(DirectoryInfo workingDirectory)
        {
            _workingDirectory = workingDirectory;
        }
        
        public void Clone(DirectoryInfo source)
        {
            Console.WriteLine("Cloning repository...");
            string timestamp = source.LastWriteTimeUtc.ToString("s").Replace(":", "");
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
    }
}
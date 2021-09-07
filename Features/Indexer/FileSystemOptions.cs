using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Filexer.Features.Indexer
{
    /// <summary>Platform and user options for scanning the file system.</summary>
    public class FileSystemOptions
    {
        private const string SyncDirectory = "tmp/sync";
        public readonly string UserHomePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        public string BackUpLocation { get; set; } = "";
        public bool CloneGitRepositories { get; set; } = true;
        
        private IList<string> _includedRootDirectories = new List<string>();
        public IList<string> IncludedRootDirectories
        {
            get => _includedRootDirectories;
            set
            {
                _includedRootDirectories = value;
                if (!_includedRootDirectories.Contains(UserHomePath))
                {
                    _includedRootDirectories.Add(UserHomePath);
                }
            }
        }

        public IReadOnlyList<string> IgnoredDirectories { get; set; } = new List<string>();
        public FileExtensions Extensions { get; set; } = new();
        
        public readonly OutputDirectories Output = new(SyncDirectory);
        
        /// <summary>Checks if directory is ignored</summary>
        /// <param name="info">the info for directory</param>
        /// <returns>true if directory is ignored</returns>
        public bool IsDirectoryIgnored(DirectoryInfo info)
        {
            if (info.Name == ".ssh")
            {
                return false;
            }

            // Exclude hidden directories
            if (info.Attributes.HasFlag(FileAttributes.Hidden))
            {
                return true;
            }

            // For now let everything through except the ignored ones
            // It is a ton of work to just include every allowed name
            return IgnoredDirectories.Contains(info.Name.ToLower());
        }
    }
}
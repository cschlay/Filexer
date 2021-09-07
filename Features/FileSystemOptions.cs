using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Filexer.Features.Indexer;

namespace Filexer.Features
{
    /// <summary>Platform and user options for scanning the file system.</summary>
    public class FileSystemOptions
    {
        private const string SyncDirectory = "tmp/sync";
        public readonly string UserHomePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        public string BackUpLocation { get; set; } = "";
        public bool CloneGitRepositories { get; set; } = true;
        public bool IncludeFilesWithoutExtension { get; set; } = true;
        
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
        public FileExtensions IncludedExtensions { get; set; } = new();
        
        // TODO: Put these into a object e.g. OutputDirectories
        public OutputDirectories Output = new(SyncDirectory);

        // TODO: Read these from a configuration file
        public readonly string[] excludedDirectoryNames =
        {
            // Standard folders
            "downloads",
            "music",
            "node_modules",
            "pictures",
            "videos",
            // System specific
            "snap"
        };

        public static readonly string[] officeExtensions =
        {
            ".docx",
            ".odt",
            ".xlsx",
            ".ods"
        };
        
        public  readonly string[] includedFileExtensions = new string[] {
            "",
            ".pub",
            ".bashrc",
            ".pdf",
            ".txt",
            ".tex",
            // Developer formats
            ".json",
            ".yaml",
            ".md",
        }.Concat(officeExtensions).ToArray();

        // TODO: Put to own class
        public bool CheckFileIgnore(string path)
        {
            string extension = Path.GetExtension(path);
            if (Array.IndexOf(includedFileExtensions, extension) > -1)
            {
                return false;
            }
            return true;
        }
        
        // TODO: Move to own class?
        public bool CheckDirectoryIgnore(DirectoryInfo info)
        {
            if (info.Name == ".ssh") return false;

            // Exclude hidden directories
            if (info.Attributes.HasFlag(FileAttributes.Hidden))
            {
                return true;
            }

            // For now let everything through except the ignored ones
            // It is a ton of work to just include every allowed name
            if (Array.IndexOf(excludedDirectoryNames, info.Name.ToLower()) > -1)
            {
                return true;
            }
            
            // TODO: Check the size
            
            return false;
        }
    }
}
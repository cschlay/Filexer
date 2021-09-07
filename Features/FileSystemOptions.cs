using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Filexer.Features
{
    /// <summary>Platform and user options for scanning the file system.</summary>
    public class FileSystemOptions
    {
        public string UserHomePath { get; set; }
        public string SyncDirectory { get; set; } = "tmp/sync";
        public string DevDirectory => $"{SyncDirectory}/dev";
        public string DocumentDirectory => $"{SyncDirectory}/documents";
        public string ProjectDirectory => $"{SyncDirectory}/projects";
        public string SecretsDirectory => $"{SyncDirectory}/secrets";
        public string OfficeDirectory => $"{SyncDirectory}/office";
        public string MiscDirectory => $"{SyncDirectory}/misc";

        public IReadOnlyList<string> IncludedDirectoryNames { get; set; }
        
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
        
        public FileSystemOptions()
        {
            UserHomePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        }
    }
}
using System;
using System.Collections.Generic;

namespace Filexer.Features
{
    /// <summary>Platform and user options for scanning the file system.</summary>
    public class FileSystemOptions
    {
        public string UserHomePath { get; set; }

        public FileSystemOptions()
        {
            UserHomePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        }        
    }
}
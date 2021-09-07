using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Filexer.Features.Indexer
{
    public enum FileCategory
    {
        Development,
        Document,
        Office,
        Secrets,
        Misc
    }
    
    public class FileExtensions
    {
        public bool IncludeFilesWithoutExtension { get; set; } = true;
        
        public IReadOnlyList<string> Development { get; set; } = new List<string>
        {
            ".md", ".json", ".sh", ".yaml"
        };

        public IReadOnlyList<string> Documents { get; set; } = new List<string>
        {
            ".pdf"
        };

        public IReadOnlyList<string> Office { get; set; } = new List<string>
        {
            ".docx", ".odt", ".ods", ".xlsx"
        };
        
        
        public IReadOnlyList<string> Secrets { get; set; } = new List<string>
        {
            ".pem", ".pub"
        };

        public IReadOnlyList<string> Misc { get; set; } = new List<string>
        {
            ".txt"
        };

        /// <summary>
        /// Returns the category the file belongs by its extension.
        /// </summary>
        /// <param name="path">the location of file</param>
        /// <returns>the category of the file or null if it should be ignored</returns>
        public FileCategory? GetFileCategory(string path)
        {
            string extension = Path.GetExtension(path);
            if (string.IsNullOrEmpty(extension) && IncludeFilesWithoutExtension)
                return FileCategory.Misc;
            if (Development.Contains(extension))
                return FileCategory.Development;
            if (Documents.Contains(extension))
                return FileCategory.Document;
            if (Office.Contains(extension))
                return FileCategory.Office;
            if (Secrets.Contains(extension))
                return FileCategory.Secrets;
            return null;
        }
    }
}

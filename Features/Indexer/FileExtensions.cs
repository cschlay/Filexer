using System.Collections.Generic;

namespace Filexer.Features.Indexer
{
    public class FileExtensions
    {
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
    }
}
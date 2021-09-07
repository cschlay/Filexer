using System;
using System.Diagnostics.CodeAnalysis;
using Filexer.Features;
using Filexer.Features.Indexer;
using Filexer.Utilities;

namespace Filexer
{
    [SuppressMessage("ReSharper", "ArrangeTypeMemberModifiers")]
    [SuppressMessage("ReSharper", "ArrangeTypeModifiers")]
    static class Program
    {
        static void Main(string[] args)
        {            
            var startTime = DateTime.UtcNow;

            FileSystemOptions options = Configuration.ReadOptions();
            var scanner = new FileSystemScanner(options);
            scanner.Index();
            
            var endTime = DateTime.UtcNow;
            TimeSpan timeElapsed = endTime - startTime;
            Console.WriteLine($"Time: {timeElapsed.TotalSeconds} seconds");
        }
    }
}

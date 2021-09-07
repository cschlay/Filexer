using System.Diagnostics.CodeAnalysis;
using Filexer.Features;
using Filexer.Utilities;

namespace Filexer
{
    [SuppressMessage("ReSharper", "ArrangeTypeMemberModifiers")]
    [SuppressMessage("ReSharper", "ArrangeTypeModifiers")]
    static class Program
    {
        static void Main(string[] args)
        {
            FileSystemOptions options = Configuration.ReadOptions();
            var scanner = new FileSystemScanner(options);
            scanner.Index();
        }
    }
}

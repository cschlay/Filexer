using System.Diagnostics.CodeAnalysis;
using Filexer.Features;

namespace Filexer
{
    [SuppressMessage("ReSharper", "ArrangeTypeMemberModifiers")]
    [SuppressMessage("ReSharper", "ArrangeTypeModifiers")]
    static class Program
    {
        static void Main(string[] args)
        {
            var options = new FileSystemOptions
            {
            };

            var scanner = new FileSystemScanner(options);
            scanner.Index();
        }
    }
}

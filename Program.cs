using System;
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
            FileSystemScanner? scanner = null;
            var options = new FileSystemOptions
            {
            };
            
            if (OperatingSystem.IsWindows())
            {
                scanner = new FileSystemScanner(options);
            }
            else if (OperatingSystem.IsLinux())
            {
                scanner = new FileSystemScanner(options);
            }

            if (scanner != null)
            {
                scanner.Index();
            }
            else
            {
                Console.WriteLine("Operating system is supported, expected Linux or Windows.");
            }
        }
    }
}

using System;

namespace Filexer.Utilities
{
    public static class Timestamp
    {
        // TODO: Actually it is better to write an extension to DateTime, e.g. ToFileTime
        
        /// <summary>
        /// Returns a string representation of string with all separators removed.
        /// </summary>
        /// <param name="dateTime">Time to convert</param>
        public static string RemoveSeparators(DateTime dateTime)
            => dateTime.ToString("s").Replace("-", "").Replace(":", "");
    }
}

using System;

namespace Filexer.Utilities
{
    public static class Timestamp
    {
        /// <summary>
        /// Returns a string representation of string with all separators removed.
        /// </summary>
        /// <param name="dateTime">Time to convert</param>
        public static string RemoveSeparators(DateTime dateTime)
            => dateTime.ToString("s").Replace("-", "").Replace(":", "");
    }
}

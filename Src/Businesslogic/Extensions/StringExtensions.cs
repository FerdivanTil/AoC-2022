using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Businesslogic.Extensions
{
    public static class StringExtensions
    {
        public static bool ContainsAny(this string self, IEnumerable<char> items)
        {
            return self.ToArray().Any(x => items.Contains(x));
        }
        public static IEnumerable<string> Split(this string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }
    }
}

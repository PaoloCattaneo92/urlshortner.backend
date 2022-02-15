using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaoloCattaneo.UrlShortner.Core.BL
{
    internal static class StringExtensions
    {
        internal static string ToArrayToString(this IEnumerable<char> charSequence)
        {
            return new string(charSequence.ToArray());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaoloCattaneo.UrlShortner.Core.BL
{
    public class ShortnerOptions
    {
        public string Alphabet { get; set; }
        public bool PadToFixedLength { get; set; }
        public int PaddingLength { get; set; }
        public char PaddingChar { get; set; }

        public int Base => Alphabet.Length;
    }
}

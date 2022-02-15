using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaoloCattaneo.UrlShortner.Core.BL
{
    public class Shortner
    {
        private readonly ShortnerOptions options;

        public Shortner(ShortnerOptions options)
        {
            this.options = options;
        }

        public string Encode(int i)
        {
            var s = new StringBuilder();
            while (i > 0)
            {
                s.Append(options.Alphabet[i % options.Base]);
                i /= options.Base;
            } 

            return options.PadToFixedLength 
                ? s.ToString().Reverse().ToArrayToString().PadLeft(options.PaddingLength, options.PaddingChar)
                : s.ToString().Reverse().ToArrayToString();
        }

        public int Decode(string s)
        {
            var i = 0;

            if(options.PadToFixedLength)
            {
                s = s.Replace(options.PaddingChar.ToString(), string.Empty);
            }

            foreach (var c in s)
            {
                i = (i * options.Base) + options.Alphabet.IndexOf(c);
            }

            return i;
        }
    }
}

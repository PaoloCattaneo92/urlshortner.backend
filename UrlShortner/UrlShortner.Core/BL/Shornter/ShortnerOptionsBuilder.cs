using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaoloCattaneo.UrlShortner.Core.BL
{
    public class ShortnerOptionsBuilder
    {
        private readonly ShortnerOptions options;

        public ShortnerOptionsBuilder()
        {
            options = new ShortnerOptions()
            {
                Alphabet = "abcdefghijklmnopqrstuvwxyz0123456789"
            };
        }

        public ShortnerOptionsBuilder WithAlphabet(string value)
        {
            if(string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            options.Alphabet = value;
            return this;
        }

        public ShortnerOptionsBuilder WithoutPadLeft()
        {
            options.PadToFixedLength = false;
            options.PaddingChar = ' ';
            options.PaddingLength = 0;
            return this;
        }

        public ShortnerOptionsBuilder WithPadLeft(char paddingChar, int padLength)
        {
            if (padLength <= 0)
            {
                throw new ArgumentException("Pad length must be a positive value");
            }

            if(true == options.Alphabet?.Contains(paddingChar))
            {
                throw new ArgumentException("Padding char cannot be a valid element of the alphabet");
            }

            options.PadToFixedLength = true;
            options.PaddingChar = paddingChar;
            options.PaddingLength = padLength;
            return this;
        }

        public ShortnerOptions Build()
        {
            return options;
        }

    }
}

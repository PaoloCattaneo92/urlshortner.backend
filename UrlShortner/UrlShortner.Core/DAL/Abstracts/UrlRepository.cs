using PaoloCattaneo.UrlShortner.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaoloCattaneo.UrlShortner.Core.DAL
{
    public abstract class UrlRepository : IUrlRepository
    {
        protected readonly TimeSpan EXPIRATION_DURATION = TimeSpan.FromDays(30 * 6);

        public abstract void Delete(int id);
        public abstract UrlShort Get(int id);
        public abstract UrlShort Insert(string url);
        public abstract UrlShort Update(int id, string key);
    }
}

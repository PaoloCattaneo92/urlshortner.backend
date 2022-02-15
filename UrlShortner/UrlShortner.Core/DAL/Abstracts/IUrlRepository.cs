using PaoloCattaneo.UrlShortner.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaoloCattaneo.UrlShortner.Core.DAL
{
    public interface IUrlRepository
    {
        UrlShort Get(int id);
        UrlShort Insert(string url);
        UrlShort Update(int id, string key);
        void Delete(int id);
    }
}

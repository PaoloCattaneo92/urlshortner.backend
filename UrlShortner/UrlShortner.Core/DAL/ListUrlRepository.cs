using PaoloCattaneo.UrlShortner.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PaoloCattaneo.UrlShortner.Core.DAL
{
    public class ListUrlRepository : UrlRepository
    {
        private int id;
        private readonly List<UrlShort> list = new();

        public override UrlShort Get(int id)
        {
            return list.FirstOrDefault(u => u.Id == id);
        }

        public override UrlShort Insert(string url)
        {
            var newId = Interlocked.Increment(ref id);
            var newItem = new UrlShort
            {
                Id = newId,
                Url = url,
            };
            list.Add(newItem);
            return newItem;
        }

        public override UrlShort Update(int id, string key)
        {
            var exsts = list.FirstOrDefault(u => u.Id == id);
            if(exsts == null)
            {
                throw new KeyNotFoundException($"Url with id {id} was not found");
            }
            exsts.Key = key;
            return exsts;
        }

        public override void Delete(int id)
        {
            var exsts = list.FirstOrDefault(u => u.Id == id);
            if(exsts != null)
            {
                list.Remove(exsts);
            }
        }
    }
}

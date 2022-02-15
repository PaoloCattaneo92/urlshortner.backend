using PaoloCattaneo.UrlShortner.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaoloCattaneo.UrlShortner.Core.DAL
{
    public class MySqlUrlRepository : UrlRepository
    {
        private static UrlShortenerContext GetContext() => new();

        public override void Delete(int id)
        {
            using (var context = GetContext())
            {
                var item = context.Url.FirstOrDefault(u => u.Id == id);
                if(item != null)
                {
                    context.Url.Remove(item);
                    context.SaveChanges();
                }
            }
        }

        public override UrlShort Get(int id)
        {
            using(var context = GetContext())
            {
                var item = context.Url.FirstOrDefault(u => u.Id == id);
                return item != null ? UrlDBMap.Mapper.Map<UrlShort>(item) : null;
            }
        }

        public override UrlShort Insert(string url)
        {
            using (var context = GetContext())
            {
                var item = new Url
                {
                    Url1 = url,
                    CreationTime = DateTime.UtcNow,
                    ExpirationTime = DateTime.UtcNow + EXPIRATION_DURATION
                };
                context.Url.Add(item);
                context.SaveChanges();
                return UrlDBMap.Mapper.Map<UrlShort>(item);
            }
        }

        public override UrlShort Update(int id, string key)
        {
            using (var context = GetContext())
            {
                var item = context.Url.FirstOrDefault(u => u.Id == id);
                if(item == null)
                {
                    throw new KeyNotFoundException($"Url shortened with id {id} was not found");
                }
                item.Key = key;
                context.SaveChanges();
                return UrlDBMap.Mapper.Map<UrlShort>(item);
            }
        }
    }
}

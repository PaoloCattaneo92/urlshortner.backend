using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaoloCattaneo.UrlShortner.Core.Model
{
    [DebuggerDisplay("{Id}: {Key} => {Url}")]
    public class UrlShort
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("key")]
        public string Key { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("creation_time")]
        public DateTime CreationTime { get; set; }
        [JsonProperty("expiration_time")]
        public DateTime ExpirationTime { get; set; }
    }
}

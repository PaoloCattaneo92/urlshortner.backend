using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace PaoloCattaneo.UrlShortner.Core.DAL
{
    public partial class Url
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Url1 { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaoloCattaneo.UrlShortner.Core.DAL
{
    public static class UrlDBMap
    {
        private static IMapper mapper;
        public static IMapper Mapper
        {
            get
            {
                if(mapper == null)
                {
                    var mapperConfig = new MapperConfiguration(mc =>
                    {
                        mc.AddProfile(new UrlDBMapProfile());
                    });
                    mapper = mapperConfig.CreateMapper();
                }
                return mapper;
            }
        }
    }
}

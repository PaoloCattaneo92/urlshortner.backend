using AutoMapper;
using PaoloCattaneo.UrlShortner.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaoloCattaneo.UrlShortner.Core.DAL
{
    public class UrlDBMapProfile : Profile
    {
        public UrlDBMapProfile()
        {
            CreateMap<Url, UrlShort>()
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url1));

            CreateMap<UrlShort, Url>()
                .ForMember(dest => dest.Url1, opt => opt.MapFrom(src => src.Url));
        }
        
    }
}

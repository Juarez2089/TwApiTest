using AutoMapper;
using TwilioApi.DTO;
using TwilioApi.Model;

namespace TwilioApi.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TwlMessage, TwlMessageDTO>().ReverseMap() ;
 
        }
    }
}
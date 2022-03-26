using aninja_rating_service.Dtos;
using aninja_rating_service.Models;
using AutoMapper;

namespace aninja_rating_service.Profiles
{
    public class AnimeProfile : Profile
    {
        public AnimeProfile()
        {
            CreateMap<AnimePublishedDto, Anime>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.Id));

        }
    }
}

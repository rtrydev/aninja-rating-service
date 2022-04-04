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
            CreateMap<GrpcAnimeModel, Anime>()
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.AnimeId))
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Anime, AnimeDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ExternalId));

        }
    }
}

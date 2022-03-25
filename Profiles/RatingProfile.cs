using aninja_rating_service.Dtos;
using aninja_rating_service.Models;
using AutoMapper;

namespace aninja_rating_service.Profiles
{
    public class RatingProfile : Profile
    {
        public RatingProfile()
        {
            CreateMap<Rating, RatingDto>();
        }
    }
}

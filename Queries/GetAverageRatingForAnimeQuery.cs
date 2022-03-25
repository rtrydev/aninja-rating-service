using MediatR;

namespace aninja_rating_service.Queries
{
    public class GetAverageRatingForAnimeQuery : IRequest<decimal>
    {
        public int AnimeId { get; set; }
    }
}

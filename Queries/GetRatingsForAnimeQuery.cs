using aninja_rating_service.Models;
using MediatR;

namespace aninja_rating_service.Queries
{
    public class GetRatingsForAnimeQuery : IRequest<IEnumerable<Rating>?>
    {
        public int AnimeId { get; set; }
    }
}

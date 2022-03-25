using aninja_rating_service.Models;
using MediatR;

namespace aninja_rating_service.Queries
{
    public class GetRatingsByUserQuery : IRequest<IEnumerable<Rating>?>
    {
        public Guid UserId { get; set; }
    }
}

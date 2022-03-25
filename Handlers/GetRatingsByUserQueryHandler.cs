using aninja_rating_service.Models;
using aninja_rating_service.Queries;
using aninja_rating_service.Repositories;
using MediatR;

namespace aninja_rating_service.Handlers
{
    public class GetRatingsByUserQueryHandler : IRequestHandler<GetRatingsByUserQuery, IEnumerable<Rating>?>
    {
        private IRatingRepository _ratingRepository;
        public GetRatingsByUserQueryHandler(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }
        public async Task<IEnumerable<Rating>?> Handle(GetRatingsByUserQuery request, CancellationToken cancellationToken)
        {
            var ratings = await _ratingRepository.GetRatingsByUser(request.UserId);
            return ratings;
        }
    }
}

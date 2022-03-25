using aninja_rating_service.Queries;
using aninja_rating_service.Repositories;
using MediatR;

namespace aninja_rating_service.Handlers
{
    public class GetAverageRatingForAnimeQueryHandler : IRequestHandler<GetAverageRatingForAnimeQuery, decimal>
    {
        private IRatingRepository _ratingRepository;
        public GetAverageRatingForAnimeQueryHandler(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }
        public async Task<decimal> Handle(GetAverageRatingForAnimeQuery request, CancellationToken cancellationToken)
        {
            var result = await _ratingRepository.GetAverageRatingForAnime(request.AnimeId);
            return result;
        }
    }
}

using aninja_rating_service.Models;
using aninja_rating_service.Queries;
using aninja_rating_service.Repositories;
using MediatR;

namespace aninja_rating_service.Handlers
{
    public class GetRatingsForAnimeQueryHandler : IRequestHandler<GetRatingsForAnimeQuery, IEnumerable<Rating>?>
    {
        private IRatingRepository _ratingRepository;
        public GetRatingsForAnimeQueryHandler(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }
        public async Task<IEnumerable<Rating>?> Handle(GetRatingsForAnimeQuery request, CancellationToken cancellationToken)
        {
            var result = await _ratingRepository.GetRatingsForAnime(request.AnimeId);
            return result;
        }
    }
}

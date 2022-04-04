using aninja_rating_service.Models;
using aninja_rating_service.Queries;
using aninja_rating_service.Repositories;
using MediatR;

namespace aninja_rating_service.Handlers;

public class GetTopAnimeQueryHandler : IRequestHandler<GetTopAnimeQuery, IEnumerable<Anime>?>
{
    private IRatingRepository _ratingRepository;

    public GetTopAnimeQueryHandler(IRatingRepository ratingRepository)
    {
        _ratingRepository = ratingRepository;
    }

    public async Task<IEnumerable<Anime>?> Handle(GetTopAnimeQuery request, CancellationToken cancellationToken)
    {
        var animes = await _ratingRepository.GetAnimes();
        animes = animes.OrderByDescending(async x => await _ratingRepository.GetAverageRatingForAnime(x.ExternalId));
        animes = animes.Count() < 10 ? animes : animes.Take(10);
        return animes;
    }
}
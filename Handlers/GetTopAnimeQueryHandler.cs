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
        var ratings = new List<Rating>();
        foreach (var anime in animes)
        {
            var value = await _ratingRepository.GetAverageRatingForAnime(anime.ExternalId);
            ratings.Add(new Rating(){AnimeId = anime.ExternalId, Mark = value});
        }
        animes = animes.OrderByDescending(x => ratings.First(y => y.AnimeId == x.ExternalId).Mark).ToArray();
        animes = animes.Count() < 10 ? animes : animes.Take(10);
        return animes;
    }
}
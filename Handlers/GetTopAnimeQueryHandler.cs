using aninja_rating_service.Dtos;
using aninja_rating_service.Models;
using aninja_rating_service.Queries;
using aninja_rating_service.Repositories;
using aninja_rating_service.SyncDataServices;
using MediatR;

namespace aninja_rating_service.Handlers;

public class GetTopAnimeQueryHandler : IRequestHandler<GetTopAnimeQuery, IEnumerable<AnimeListItemDto>?>
{
    private class RatingForAnime
    {
        public int Id { get; set; }
        public decimal Rating { get; set; }
    }
    
    private IRatingRepository _ratingRepository;
    private IHttpAnimeDataClient _animeClient;
    public GetTopAnimeQueryHandler(IRatingRepository ratingRepository, IHttpAnimeDataClient animeClient)
    {
        _ratingRepository = ratingRepository;
        _animeClient = animeClient;
    }

    public async Task<IEnumerable<AnimeListItemDto>?> Handle(GetTopAnimeQuery request, CancellationToken cancellationToken)
    {
        var animes = request.Filter == "Airing" ? (await _animeClient.GetAiringAnimes()).Animes : (await _animeClient.GetAllAnimes()).Animes;
        var ratings = animes.Select(x => new RatingForAnime() {Id = x.Id, Rating = 0m}).ToArray();
        for (int i = 0; i < ratings.Count(); i++)
        {
            ratings[i].Rating = await _ratingRepository.GetAverageRatingForAnime(ratings[i].Id);
        }

        animes = animes.OrderByDescending(x => ratings.First(y => y.Id == x.Id).Rating).ToArray();

        var topAnimes = animes.Length < 10 ? animes : animes.Take(10).ToArray();
        for (int i = 0; i < topAnimes.Length; i++)
        {
            topAnimes[i].Rating = ratings.First(x => x.Id == topAnimes[i].Id).Rating;
        }

        return topAnimes;
    }
}
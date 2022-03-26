using aninja_rating_service.Models;

namespace aninja_rating_service.SyncDataServices;

public interface IAnimeDataClient
{
    IEnumerable<Anime> ReturnAllAnime();
}
using aninja_rating_service.Models;

namespace aninja_rating_service.Repositories;

public interface IRatingRepository
{
    public IEnumerable<Rating> GetRatingsForAnime(int animeId);
    public IEnumerable<Rating> GetRatingsByUser(Guid userId);
    public decimal GetAverageRatingForAnime(int animeId);
}
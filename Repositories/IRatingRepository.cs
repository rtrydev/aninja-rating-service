using aninja_rating_service.Models;

namespace aninja_rating_service.Repositories;

public interface IRatingRepository
{
    public Task<IEnumerable<Rating>> GetRatingsForAnime(int animeId);
    public Task<IEnumerable<Rating>> GetRatingsByUser(Guid userId);
    public Task<decimal> GetAverageRatingForAnime(int animeId);
}
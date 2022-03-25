using aninja_rating_service.Models;

namespace aninja_rating_service.Repositories;

public class RatingRepository : IRatingRepository
{
    public IEnumerable<Rating> GetRatingsForAnime(int animeId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Rating> GetRatingsByUser(Guid userId)
    {
        throw new NotImplementedException();
    }

    public decimal GetAverageRatingForAnime(int animeId)
    {
        throw new NotImplementedException();
    }
}
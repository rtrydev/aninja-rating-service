using aninja_rating_service.Models;
using MongoDB.Driver;

namespace aninja_rating_service.Repositories;

public class RatingRepository : IRatingRepository
{
    private IMongoCollection<Rating> _collection;

    public RatingRepository(IMongoClient mongoClient)
    {
        _collection = mongoClient.GetDatabase("ratingDB").GetCollection<Rating>("ratings");
    }

    public async Task<IEnumerable<Rating>> GetRatingsForAnime(int animeId)
    {
        var cursor = await _collection.FindAsync(x => x.AnimeId == animeId);
        var result = await cursor.ToListAsync();
        return result;
    }

    public async Task<IEnumerable<Rating>> GetRatingsByUser(Guid userId)
    {
        var cursor = await _collection.FindAsync(x => x.SubmitterId == userId);
        var result = await cursor.ToListAsync();
        return result;
    }

    public async Task<decimal> GetAverageRatingForAnime(int animeId)
    {
        var cursor = await _collection.FindAsync(x => x.AnimeId == animeId);
        var ratings = await cursor.ToListAsync();
        if (ratings is null || ratings.Count == 0) return new decimal(0);
        return ratings.Average(x => x.Mark);
    }
}
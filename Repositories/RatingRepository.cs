using aninja_rating_service.Models;
using MongoDB.Driver;

namespace aninja_rating_service.Repositories;

public class RatingRepository : IRatingRepository
{
    private IMongoCollection<Rating> _ratingCollection;
    private IMongoCollection<Anime> _animeCollection;

    public RatingRepository(IMongoClient mongoClient)
    {
        _ratingCollection = mongoClient.GetDatabase("ratingDB").GetCollection<Rating>("ratings");
        _animeCollection = mongoClient.GetDatabase("ratingDB").GetCollection<Anime>("animes");
    }

    public async Task<IEnumerable<Rating>> GetRatingsForAnime(int animeId)
    {
        var cursor = await _ratingCollection.FindAsync(x => x.AnimeId == animeId);
        var result = await cursor.ToListAsync();
        return result;
    }

    public async Task<IEnumerable<Rating>> GetRatingsByUser(Guid userId)
    {
        var cursor = await _ratingCollection.FindAsync(x => x.SubmitterId == userId);
        var result = await cursor.ToListAsync();
        return result;
    }

    public async Task<decimal> GetAverageRatingForAnime(int animeId)
    {
        var cursor = await _ratingCollection.FindAsync(x => x.AnimeId == animeId);
        var ratings = await cursor.ToListAsync();
        if (ratings is null || ratings.Count == 0) return new decimal(0);
        return ratings.Average(x => x.Mark);
    }

    public async Task<Rating?> AddRating(Rating rating)
    {
        if(!(await AnimeExists(rating.AnimeId))) return null;
        await _ratingCollection.InsertOneAsync(rating);
        return rating;
    }

    public async Task RemoveRating(Rating rating)
    {
        await _ratingCollection.DeleteOneAsync(x => x.Id == rating.Id);
    }

    public async Task<Anime?> AddAnime(Anime anime)
    {
        if (await AnimeExists(anime.ExternalId)) return null;
        await _animeCollection.InsertOneAsync(anime);
        return anime;
    }

    public async Task<Anime?> UpdateAnime(Anime anime)
    {
        var animeInDb = await GetAnime(anime.ExternalId);
        if (animeInDb is null) return null;
        animeInDb.Title = anime.Title;
        var result = await _animeCollection.FindOneAndReplaceAsync(x => x.ExternalId == anime.ExternalId, animeInDb);
        return result;
    }

    public async Task DeleteAnime(int animeId)
    {
        await _animeCollection.DeleteOneAsync(x => x.ExternalId == animeId);
    }

    public async Task<bool> AnimeExists(int animeId)
    {
        var cursor = await _animeCollection.FindAsync(x => x.ExternalId == animeId);
        var anime = await cursor.FirstOrDefaultAsync();
        return anime is not null;
    }

    public async Task<Anime?> GetAnime(int animeId)
    {
        var cursor = await _animeCollection.FindAsync(x => x.ExternalId == animeId);
        var anime = await cursor.FirstOrDefaultAsync();
        return anime;
    }
}
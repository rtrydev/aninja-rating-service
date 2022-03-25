using aninja_rating_service.Models;

namespace aninja_rating_service.Repositories;

public interface IRatingRepository
{
    public Task<IEnumerable<Rating>> GetRatingsForAnime(int animeId);
    public Task<IEnumerable<Rating>> GetRatingsByUser(Guid userId);
    public Task<decimal> GetAverageRatingForAnime(int animeId);

    public Task<Rating?> AddRating(Rating rating);
    public Task RemoveRating(Rating rating);

    public Task<Anime?> GetAnime(int animeId);
    public Task<Anime?> AddAnime(Anime anime);
    public Task<Anime?> UpdateAnime(Anime anime);
    public Task DeleteAnime(int animeId);
    public Task<bool> AnimeExists(int animeId);
}
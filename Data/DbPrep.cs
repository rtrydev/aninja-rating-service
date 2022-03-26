using aninja_rating_service.Models;
using aninja_rating_service.Repositories;
using aninja_rating_service.SyncDataServices;

namespace aninja_rating_service.Data;

public class DbPrep
{
    public static async Task PrepData(IApplicationBuilder app, bool isProduction)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            if (isProduction)
            {
                var grpcClient = serviceScope.ServiceProvider.GetService<IAnimeDataClient>();
                if (grpcClient is not null)
                {
                    var anime = grpcClient.ReturnAllAnime(); 
                    await SeedData(serviceScope.ServiceProvider.GetService<IRatingRepository>()!, anime); 
                }
            }
        }
    }

    private static async Task SeedData(IRatingRepository ratingRepository, IEnumerable<Anime> anime)
    {
        foreach (var a in anime)
        {
            if (!await ratingRepository.AnimeExists(a.ExternalId))
            {
                await ratingRepository.AddAnime(a);
            }
        }
    }
}
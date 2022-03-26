using System.Text.Json;
using aninja_rating_service.Dtos;
using aninja_rating_service.Models;
using aninja_rating_service.Repositories;
using AutoMapper;

namespace aninja_rating_service.EventProcessing;

enum EventType
{
    AnimePublished,
    AnimeUpdated,
    Undetermined
}

public class EventProcessor : IEventProcessor
{
    private readonly IMapper _mapper;
    private readonly IServiceScopeFactory _scopeFactory;

    public EventProcessor(IMapper mapper, IServiceScopeFactory scopeFactory)
    {
        _mapper = mapper;
        _scopeFactory = scopeFactory;
    }

    public async Task ProcessEvent(string message)
    {
        var eventType = DetermineEvent(message);
        switch (eventType)
        {
            case EventType.AnimePublished:
                await AddAnime(message);
                break;
            case EventType.AnimeUpdated:
                await UpdateAnime(message);
                break;
            default:
                break;
        }
    }

    private async Task AddAnime(string message)
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var repo = scope.ServiceProvider.GetRequiredService<IRatingRepository>();
            var animePublishedDto = JsonSerializer.Deserialize<AnimePublishedDto>(message);
            var anime = _mapper.Map<Anime>(animePublishedDto);
            if (!await repo.AnimeExists(anime.ExternalId))
            {
                await repo.AddAnime(anime);
            }
        }
    }

    private async Task UpdateAnime(string message)
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var repo = scope.ServiceProvider.GetRequiredService<IRatingRepository>();
            var animePublishedDto = JsonSerializer.Deserialize<AnimePublishedDto>(message);
            var anime = _mapper.Map<Anime>(animePublishedDto);
            if (await repo.AnimeExists(anime.ExternalId))
            {
                await repo.UpdateAnime(anime);
            }
        }
    }

    private EventType DetermineEvent(string message)
    {
        var eventType = JsonSerializer.Deserialize<GenericEventDto>(message);
        return eventType?.Event switch
        {
            "Anime_Published" => EventType.AnimePublished,
            "Anime_Updated" => EventType.AnimeUpdated,
            _ => EventType.Undetermined
        };
    }
}
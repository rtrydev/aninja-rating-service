using System.Text.Json;
using System.Text.Json.Serialization;
using aninja_rating_service.Dtos;
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace aninja_rating_service.SyncDataServices;

public interface IHttpAnimeDataClient
{
    public Task<AnimeServiceResponseDto> GetAllAnimes();
    public Task<AnimeServiceResponseDto> GetAiringAnimes();
}

public class HttpAnimeDataClient : IHttpAnimeDataClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public HttpAnimeDataClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }


    public async Task<AnimeServiceResponseDto?> GetAllAnimes()
    {
        var response = await _httpClient.GetAsync(_configuration["AnimeService"] + "?resultsPerPage=1000000");
        if (!response.IsSuccessStatusCode) return null;
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<AnimeServiceResponseDto>(content);
        return result;
    }

    public async Task<AnimeServiceResponseDto> GetAiringAnimes()
    {
        var response = await _httpClient.GetAsync(_configuration["AnimeService"] + "?resultsPerPage=1000000&statuses=CurrentlyAiring");
        if (!response.IsSuccessStatusCode) return null;
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<AnimeServiceResponseDto>(content);
        return result;
    }
}
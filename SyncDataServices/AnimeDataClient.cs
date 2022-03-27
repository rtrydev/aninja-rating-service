using aninja_rating_service.Models;
using AutoMapper;
using Grpc.Net.Client;

namespace aninja_rating_service.SyncDataServices;

public class AnimeDataClient : IAnimeDataClient
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public AnimeDataClient(IConfiguration configuration, IMapper mapper)
    {
        _configuration = configuration;
        _mapper = mapper;
    }

    public IEnumerable<Anime> ReturnAllAnime()
    {
        var channel = GrpcChannel.ForAddress(_configuration["GrpcPlatform"]);
        var client = new GrpcAnime.GrpcAnimeClient(channel);
        var request = new GetAllRequest();

        var reply = client.GetAllAnime(request);
        return _mapper.Map<IEnumerable<Anime>>(reply.Anime);
    }
}
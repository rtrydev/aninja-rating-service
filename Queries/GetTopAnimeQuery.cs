using aninja_rating_service.Models;
using MediatR;

namespace aninja_rating_service.Queries;

public class GetTopAnimeQuery : IRequest<IEnumerable<Anime>?>
{
    
}
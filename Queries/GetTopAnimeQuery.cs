using aninja_rating_service.Dtos;
using aninja_rating_service.Models;
using MediatR;

namespace aninja_rating_service.Queries;

public class GetTopAnimeQuery : IRequest<IEnumerable<AnimeListItemDto>?>
{
    public string Filter { get; set; }
}
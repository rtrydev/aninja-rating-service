namespace aninja_rating_service.Dtos;

public class AnimeServiceResponseDto
{
    public AnimeListItemDto[] Animes { get; set; }
    public int AllCount { get; set; }
}
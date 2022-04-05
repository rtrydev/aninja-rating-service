namespace aninja_rating_service.Dtos;

public class AnimeListItemDto
{
    public int Id { get; set; }
    public string? TranslatedTitle { get; set; }
    public string? ImgUrl { get; set; }
    public decimal Rating { get; set; }
}
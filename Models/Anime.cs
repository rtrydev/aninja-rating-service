namespace aninja_rating_service.Models
{
    public class Anime
    {
        public Guid Id { get; set; }
        public int ExternalId { get; set; }
        public string Title { get; set; } = "";
    }
}

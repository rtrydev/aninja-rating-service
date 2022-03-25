namespace aninja_rating_service.Dtos
{
    public class RatingDto
    {
        public Guid SubmitterId { get; set; }
        public decimal Mark { get; set; }
        public int AnimeId { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string Comment { get; set; }
    }
}

using MongoDB.Bson.Serialization.Attributes;

namespace aninja_rating_service.Models;

public class Rating
{
    public Guid Id { get; set; }
    public Guid SubmitterId { get; set; }
    public int AnimeId { get; set; }
    [BsonElement]
    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime SubmissionDate { get; set; }
    public decimal Mark { get; set; }
    public string Comment { get; set; } = "";
}
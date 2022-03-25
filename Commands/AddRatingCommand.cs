using aninja_rating_service.Models;
using MediatR;

namespace aninja_rating_service.Commands
{
    public class AddRatingCommand : IRequest<Rating?>
    {
        public Guid SubmitterId { get; set; }
        public int AnimeId { get; set; }
        public decimal Mark { get; set; }
        public string Comment { get; set; }
    }
}

using aninja_rating_service.Commands;
using aninja_rating_service.Models;
using aninja_rating_service.Repositories;
using MediatR;

namespace aninja_rating_service.Handlers
{
    public class AddRatingCommandHandler : IRequestHandler<AddRatingCommand, Rating?>
    {
        private IRatingRepository _ratingRepository;
        public AddRatingCommandHandler(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }
        public async Task<Rating?> Handle(AddRatingCommand request, CancellationToken cancellationToken)
        {
            var rating = new Rating()
            {
                Id = Guid.NewGuid(),
                AnimeId = request.AnimeId,
                SubmissionDate = DateTime.Now,
                Mark = request.Mark,
                Comment = request.Comment,
                SubmitterId = request.SubmitterId
            };
            var ratinginDb = await _ratingRepository.GetRatingsByUser(request.SubmitterId);
            if (ratinginDb.Any(x => x.AnimeId == request.AnimeId)) return null;

            var result = await _ratingRepository.AddRating(rating);
            return result;

        }
    }
}

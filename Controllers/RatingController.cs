using aninja_rating_service.Models;
using aninja_rating_service.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace aninja_rating_service.Controllers
{
    [ApiController]
    [Route("api/r")]
    public class RatingController : ControllerBase
    {
        private IMediator _mediator;
        public RatingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("anime/{animeId}/rating")]
        public async Task<ActionResult<IEnumerable<Rating>>> GetRatingsForAnime(int animeId)
        {
            var request = new GetRatingsForAnimeQuery
            {
                AnimeId = animeId
            };
            var result = await _mediator.Send(request);
            if (result is null) return NotFound();
            return Ok(result);
        }
    }
}

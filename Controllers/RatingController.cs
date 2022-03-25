using aninja_rating_service.Commands;
using aninja_rating_service.Dtos;
using aninja_rating_service.Models;
using aninja_rating_service.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace aninja_rating_service.Controllers
{
    [ApiController]
    [Route("api/r")]
    public class RatingController : ControllerBase
    {
        private IMediator _mediator;
        private IMapper _mapper;
        public RatingController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("anime/{animeId}/rating")]
        public async Task<ActionResult<IEnumerable<RatingDto>>> GetRatingsForAnime(int animeId)
        {
            var request = new GetRatingsForAnimeQuery
            {
                AnimeId = animeId
            };
            var result = await _mediator.Send(request);
            if (result is null) return NotFound();
            return Ok(_mapper.Map<IEnumerable<RatingDto>>(result));
        }

        [HttpPost("anime/{animeId}/rating")]
        public async Task<ActionResult<RatingDto>> AddRating([FromBody] RatingAddDto ratingAddDto, int animeId)
        {
            //var userId = User.Claims.First(x => x.Type == "id").Value;
            var userId = "4E5CA946-7001-4BC2-A8FA-A197208EF394";
            var request = new AddRatingCommand()
            {
                SubmitterId = new Guid(userId),
                AnimeId = animeId,
                Mark = ratingAddDto.Mark,
                Comment = ratingAddDto.Comment
            };
            var result = await _mediator.Send(request);
            if(result is null) return Forbid();
            return Ok(_mapper.Map<RatingDto>(result));
        }
        [HttpGet("anime/{animeId}/rating/avg")]
        public async Task<ActionResult<decimal>> GetAverageRating(int animeId)
        {
            var query = new GetAverageRatingForAnimeQuery()
            {
                AnimeId = animeId
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("user/{userId}/rating")]
        public async Task<ActionResult<IEnumerable<RatingDto>>> GetRatingsByUser(string userId)
        {
            Guid userGuid;
            try
            {
                userGuid = Guid.Parse(userId);
            } catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            var query = new GetRatingsByUserQuery()
            {
                UserId = userGuid
            };
            var result = await _mediator.Send(query);
            if (result is null) return NotFound();
            return Ok(result);
        }
    }
}

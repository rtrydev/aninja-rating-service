using System.ComponentModel.DataAnnotations;

namespace aninja_rating_service.Dtos
{
    public class RatingAddDto
    {
        [Range(1, 10)]
        public decimal Mark { get; set; }
        [MaxLength(500)]
        public string Comment { get; set; }
    }
}

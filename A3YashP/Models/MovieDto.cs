using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A3YashP.Models
{
	public class MovieDto
    {
        [Required, StringLength(60, MinimumLength = 2, ErrorMessage = "The MovieName must be between 2 and 60 characters.")]
        public string MovieName { get; set; } = "";

        [Required, StringLength(60, MinimumLength = 3, ErrorMessage = "The MovieName must be between 3 and 60 characters.")]
        public string DirectorName { get; set; } = "";

        [Required, Range(1000, 9999, ErrorMessage = "The Duration must be a 4-digit number.")]
        public int Duration { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required, RegularExpression(@"^[A-Za-z]{3}-\d{5}-[A-Za-z]\d$", ErrorMessage = "The MovieCode must follow the format XXX-11111-X1.")]
        public string MovieCode { get; set; } = "";


        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public string MovieImageData { get; set; }
    }
}

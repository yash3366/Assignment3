using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace A3YashP.Models
{
	public class Movie
	{
		public int Id { get; set; }

        [StringLength(60, MinimumLength = 2, ErrorMessage = "The MovieName must be between 2 and 60 characters.")]
        public string MovieName { get; set; } = "";

        [StringLength(60, MinimumLength = 3, ErrorMessage = "The MovieName must be between 3 and 60 characters.")]
        public string DirectorName { get; set; } = "";

		[Range(1000, 9999, ErrorMessage = "The Duration must be a 4-digit number.")]
		public int Duration { get; set; }

		public DateTime ReleaseDate { get; set; }

		[RegularExpression(@"^[A-Za-z]{3}-\d{5}-[A-Za-z]\d$", ErrorMessage = "The MovieCode must follow the format XXX-11111-X1.")]
		public string MovieCode { get; set; } = "";

		public string ImageFile { get; set; } = "";
	}
}

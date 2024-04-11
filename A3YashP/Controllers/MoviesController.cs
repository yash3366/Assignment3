using A3YashP.Models;
using A3YashP.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace A3YashP.Controllers
{
	public class MoviesController : Controller
	{
		private readonly ApplicationDbContext context;
		private readonly IWebHostEnvironment environment;

		public MoviesController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
			this.context = context;
			this.environment = environment;
		}
        public IActionResult Index()
		{
			var movies = context.Movies.OrderByDescending(p => p.Id).ToList();
			return View(movies);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(MovieDto movieDto)
		{
			if (movieDto.ImageFile == null) 
			{
				ModelState.AddModelError("ImageFile", "The image file is required");
			}

			if (!ModelState.IsValid) 
			{ 
				return View(movieDto);
			}


			if (movieDto.ImageFile != null)
			{
				string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/movies");
				//create folder if not exist
				if (!Directory.Exists(path))
					Directory.CreateDirectory(path);
				string fileName = Guid.NewGuid().ToString() + "_" + movieDto.ImageFile.FileName;
				string fileNameWithPath = Path.Combine(path, fileName);
				using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
				{
					movieDto.ImageFile.CopyTo(stream);
				}
				movieDto.MovieImageData = fileName;
			}


			Movie movie = new Movie()
			{
				MovieName = movieDto.MovieName,
				DirectorName = movieDto.DirectorName,
				Duration = movieDto.Duration,
				ReleaseDate = movieDto.ReleaseDate,
				MovieCode = movieDto.MovieCode,
				ImageFile = movieDto.MovieImageData,
			};


			context.Movies.Add(movie);
			context.SaveChanges();

				return RedirectToAction("Index", "Movies");
		}

		public IActionResult Edit(int id) 
		{
			var movie = context.Movies.Find(id);

			if (movie == null)
			{
				return RedirectToAction("Index", "Movies");
			}

			var movieDto = new MovieDto()
			{
				MovieName = movie.MovieName,
				DirectorName = movie.DirectorName,
				Duration = movie.Duration,
				ReleaseDate = DateTime.Now,
				MovieCode = movie.MovieCode,
			};



			ViewData["MovieId"] = movie.Id;
			ViewData["ImageFile"] = movie.ImageFile;
			ViewData["ReleaseDate"] = movie.ReleaseDate.ToString("MM/dd/yyyy");

			return View(movieDto);
		}


		[HttpPost]
		public IActionResult Edit(int id, MovieDto movieDto)
		{
			var movie = context.Movies.Find(id);

			if (movie == null)
			{
				return RedirectToAction("Index", "Movies");
			}


			if (!ModelState.IsValid)
			{
				ViewData["MovieId"] = movie.Id;
				ViewData["ImageFile"] = movie.ImageFile;
				ViewData["ReleaseDate"] = movie.ReleaseDate.ToString("MM/dd/yyyy");

				return View(movieDto);
			}

            movie.MovieName = movieDto.MovieName;
			movie.DirectorName = movieDto.DirectorName;
			movie.Duration = movieDto.Duration;
			movie.MovieCode = movieDto.MovieCode;
			
			

			context.SaveChanges();

			return RedirectToAction("Index", "Movies");
		}

		public IActionResult Search(string searchString)
		{
			// Query the database based on the search string
			var movies = context.Movies
				.Where(m => m.MovieName.Contains(searchString) ||
							m.DirectorName.Contains(searchString) )
				.ToList();

			return View(movies);
		}
	}
}

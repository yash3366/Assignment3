using System;
using A3YashP.Models;

namespace A3YashP.Services
{
	public class DbInitializer
	{
		public static void Initialize(ApplicationDbContext context)
		{
			context.Database.EnsureCreated();

			if (context.Movies.Any())
			{
				return;
			}

			var movies = new Movie[]
			{
				new Movie { MovieName="Dune", DirectorName="Denis Villeneuve", Duration=1550, ReleaseDate=DateTime.Parse("2024-04-07"), MovieCode="MNO-89012-W8", ImageFile="dune.jpg"},
				new Movie { MovieName="Scoop", DirectorName="Philip Martin", Duration=1030, ReleaseDate=DateTime.Parse("2023-03-13"), MovieCode="XYZ-78901-M3", ImageFile="scoop.jpg"}
			};

			context.Movies.AddRange(movies);
			context.SaveChanges();
		}
	}
}

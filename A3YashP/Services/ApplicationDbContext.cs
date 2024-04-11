using System;
using A3YashP.Models;
using Microsoft.EntityFrameworkCore;

namespace A3YashP.Services
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options) 
		{
			
		}

		public DbSet<Movie> Movies { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Movie>().ToTable("Movies");
		}

		public DbSet<A3YashP.Models.Movie> Movie { get; set; } = default!;
	}
}

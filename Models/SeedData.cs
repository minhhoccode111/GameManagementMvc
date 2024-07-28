// using System;
// using System.Linq;
// using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;

namespace MvcMovie.Models
{
    public static class SeedData
    {
        // IServiceProvider: Defines a mechanism for retrieving a service object
        // that is, an object that provides custom support to other objects\.
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (
                var context = new MvcMovieContext(
                    serviceProvider.GetRequiredService<DbContextOptions<MvcMovieContext>>()
                )
            )
            {
                // if any movie exists
                if (context.Movie.Any())
                {
                    return; // DB has been seeded
                }

                // if no movie exists

                // AddRange:
                // Begins tracking the given entities, and any other reachable
                // entities that are not already being tracked, in the
                // `EntityState.Added` state such that they will be inserted
                // into the database when `DbContext.SaveChanges()` is called
                context.Movie.AddRange(
                    new Movie
                    {
                        Title = "When Harry Met Sally",
                        ReleaseDate = DateTime.Parse("1989-2-12"),
                        Genre = "Romantic Comedy",
                        Rating = "R",
                        Price = 7.99M
                    },
                    new Movie
                    {
                        Title = "Ghostbusters ",
                        ReleaseDate = DateTime.Parse("1984-3-13"),
                        Genre = "Comedy",
                        Rating = "R",
                        Price = 8.99M
                    },
                    new Movie
                    {
                        Title = "Ghostbusters 2",
                        ReleaseDate = DateTime.Parse("1986-2-23"),
                        Genre = "Comedy",
                        Rating = "R",
                        Price = 9.99M
                    },
                    new Movie
                    {
                        Title = "Rio Bravo",
                        ReleaseDate = DateTime.Parse("1959-4-15"),
                        Genre = "Western",
                        Rating = "R",
                        Price = 3.99M
                    }
                );

                // save changes make to _context
                context.SaveChanges();
            }
        }
    }
}

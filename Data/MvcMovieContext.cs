// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

// using MvcMovie.Models;

namespace MvcMovie.Data
{
    // A DbContext instance represents a session with the database and can be
    // used to query and save instances of your entities
    // DbContext is a combination of the Unit Of Work and Repository patterns
    public class MvcMovieContext : DbContext
    {
        // Contructor method is used to config the database connection
        public MvcMovieContext(DbContextOptions<MvcMovieContext> options)
            : base(options) { }

        // Movie property is used to query and interact with Movie Model in db
        public DbSet<MvcMovie.Models.Movie> Movie { get; set; } = default!;
    }
}

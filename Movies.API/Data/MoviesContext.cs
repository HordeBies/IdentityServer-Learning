using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Movies.API.Model;

namespace Movies.API.Data
{
    public class MoviesContext : DbContext
    {
        public MoviesContext (DbContextOptions<MoviesContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; } = default!;
    }
}

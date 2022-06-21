using Microsoft.EntityFrameworkCore;

namespace Moviepedia.Models
{
    public class MovieContext: DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public MovieContext(DbContextOptions<MovieContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }
    }
}

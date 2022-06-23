using Microsoft.EntityFrameworkCore;

namespace Moviepedia.Models
{
    public class MovieContext: DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<MoviesOnGenre> MoviesOnGenre { get; set; }
        public MovieContext(DbContextOptions<MovieContext> options)
            :base(options)
        {
            Database.EnsureCreated();
            //var genres = new Genre[]
            //{
            //    new Genre{Name="Action"},
            //    new Genre{Name="Comedy"},
            //    new Genre{Name="Drama"},
            //    new Genre{Name="Fantasy"},
            //    new Genre{Name="Sci-fi"},
            //    new Genre{Name="Horror"},
            //    new Genre{Name="Thriller"},
            //    new Genre{Name="Mystery"},
            //    new Genre{Name="Musical"},
            //    new Genre{Name="Biography"},
            //    new Genre{Name="Anime"},
            //    new Genre{Name="Adventure"},
            //    new Genre{Name="Crime"}
            //};
            //foreach (var genre in genres)
            //    Genres.Add(genre);
            //SaveChanges();
            //var movies = new Movie[]
            //{
            //    new Movie{Name="Interstellar",  Director="Cristoper Nolan", Language="English", Year=2014},
            //    new Movie{Name="Midsommar",  Director="Ari Aster", Language="English", Year=2019},
            //    new Movie{Name="Se7en",  Director="David Fincher", Language="English", Year=1995},
            //    new Movie{Name="Another round",  Director="Thomas Vinterberg", Language="Danish", Year=2020}
            //};
            //foreach (var movie in movies)
            //    Movies.Add(movie);
            //SaveChanges();
            //var moviesOnGenre = new MoviesOnGenre[]
            //{
            //    new MoviesOnGenre{Movie = movies[0], Genre= genres[2]},
            //    new MoviesOnGenre{Movie = movies[0], Genre= genres[4]},
            //    new MoviesOnGenre{Movie = movies[0], Genre= genres[11]},
            //    new MoviesOnGenre{Movie = movies[1], Genre= genres[2]},
            //    new MoviesOnGenre{Movie = movies[1], Genre= genres[7]},
            //    new MoviesOnGenre{Movie = movies[1], Genre= genres[12]},
            //    new MoviesOnGenre{Movie = movies[2], Genre= genres[2]},
            //    new MoviesOnGenre{Movie = movies[2], Genre= genres[5]},
            //    new MoviesOnGenre{Movie = movies[2], Genre= genres[7]},
            //    new MoviesOnGenre{Movie = movies[3], Genre= genres[1]},
            //    new MoviesOnGenre{Movie = movies[3], Genre= genres[2]}
            //};
            //foreach (var movieOnGenre in moviesOnGenre)
            //    MoviesOnGenre.Add(movieOnGenre);
            //SaveChanges();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MoviesOnGenre>()
                .HasKey(m => new { m.GenreId, m.MovieId });
        }
    }
}

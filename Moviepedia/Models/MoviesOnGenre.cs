using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Moviepedia.Models
{
    public class MoviesOnGenre
    {
        [HiddenInput(DisplayValue = false)]
        public int GenreId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public Genre Genre { get; set; }

    }
}

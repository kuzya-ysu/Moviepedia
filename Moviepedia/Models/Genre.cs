using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace Moviepedia.Models
{
    public class Genre
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the genre name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Input length must be betwenn 3 and 50")]
        [Display(Name = "Genre")]
        public string Name { get; set; } 

        public IEnumerable<Movie>? Movies { get; set; }
        [NotMapped]
        public IEnumerable<int>? MoviesId { get; set; }
    } 
}

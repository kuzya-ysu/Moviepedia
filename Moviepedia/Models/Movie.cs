using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moviepedia.Models
{
    [Table("Movies")]
    public class Movie
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Input length must be betwenn 3 and 50")]
        [Display(Name = "Director")]
        public string Director { get; set; } // режиссер

        [Required(ErrorMessage = "Name required")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "Input length must be betwenn 3 and 250")]
        [Display(Name = "Name")]
        public string Name { get; set; } // название
        
        [Required]
        [Range(1895, 2022, ErrorMessage = "Invalid year input")]
        [Display(Name = "Year of release")]
        public int Year { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? CategoryId { get; set; }

        [Display(Name = "Genre")]
        public Genre Genre { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageUrl { get; set; }

        [Display(Name="Main language")]
        public string Language { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Moviepedia.Models
{
    public class Movie
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name required")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "Input length must be betwenn 3 and 250")]
        [Display(Name = "Name")]
        public string Name { get; set; } // название

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Input length must be betwenn 3 and 50")]
        [Display(Name = "Director")]
        public string Director { get; set; } // режиссер
        
        [Required]
        [Range(1895, 2022, ErrorMessage = "Invalid year input")]
        [Display(Name = "Year of release")]
        public int Year { get; set; }

        [Required]
        [Display(Name="Main language")]
        public string Language { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace Moviepedia.Models
{
    public class Review
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int MovieId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Movie Movie { get; set; }

        [ForeignKey("AspNetUsers")]
        [HiddenInput(DisplayValue = false)]
        public string UserId { get; set; }

        [StringLength(800, MinimumLength = 3, ErrorMessage = "Review must be between 3 and 800 symbols long")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Review text")]
        public string? Text { get; set; }

        [Required]
        [Display(Name = "Rating")]
        public int Rating { get; set; }
    }
}

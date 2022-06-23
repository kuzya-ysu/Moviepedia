using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Moviepedia.Models
{
    public class Review
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [HiddenInput(DisplayValue = false)]
        public Movie Movie { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string AuthorId { get; set; }

        [Required]
        [StringLength(2000, MinimumLength = 10, ErrorMessage = "Review must be between 10 and 2000 symbols long")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Review text")]
        public string Text { get; set; }
    }
}

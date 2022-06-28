using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Display(Name = "Language")]
        public string Language { get; set; }

        [Display(Name = "Rating")]
        public float? Rating 
        { 
            get
            {
                float sum = 0;
                int count = 0;
                if (Reviews == null || Reviews.Count() == 0)
                    return 0;
                foreach (Review review in Reviews)
                {
                    sum+=review.Rating;
                    count++;
                }
                float rating = (float)Math.Round(sum / count, 1);
                return rating;
            }
        }
        
        public IEnumerable<Genre>? Genres { get; set; }
        [NotMapped]
        public IEnumerable<int>? GenresId { get; set; }
        public IEnumerable<Review>? Reviews { get; set; }
    }
}

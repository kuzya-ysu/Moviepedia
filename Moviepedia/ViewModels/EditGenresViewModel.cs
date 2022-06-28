using Moviepedia.Models;

namespace Moviepedia.ViewModels
{
    public class EditGenresViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }
        public IEnumerable<Genre> MovieGenres { get; set; }
        public int Id { get; set; }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moviepedia.Models;
using Moviepedia.ViewModels;

namespace Moviepedia.Controllers
{
    [Authorize(Roles = "manager")]
    public class ManagerController : Controller
    {
        private readonly MovieContext _context;

        public ManagerController(MovieContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> GetMovies()
        {

              return _context.Movies != null ? 
                          View(await _context.Movies.ToListAsync()) :
                          Problem("Entity set 'MovieContext.Movies'  is null.");

        }

        public IActionResult CreateMovie()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMovie([Bind("Id,Name,Director,Year,Language,Rating")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(GetMovies));
            }
            return View(movie);
        }

        public IActionResult EditMovie(int? id)
        {
            if (id == null)
                return NotFound();
            var movie = _context.Movies.ToList().Find(m=>m.Id == id);
            if (movie == null)
                return NotFound();
            return View(movie);
        }

        [HttpPost]
        public IActionResult EditMovie(int id, [Bind("Id,Name,Director,Year,Language")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(GetMovies));
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            return View(movie);
        }

        public IActionResult EditGenres(int? id)
        {
            if (id == null)
                return NotFound();
            var movie = _context.Movies.Include(m=>m.Genres).ToList().Find(m => m.Id == id);
            if (movie == null)
                return NotFound();
            var movieGenres = movie.Genres.ToList();
            var genres = _context.Genres.ToList();
            var model = new EditGenresViewModel
            {
                Genres = genres,
                MovieGenres = movieGenres,
                Id = movie.Id
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult EditGenres(int id, List<string> genres)
        {
            var movie = _context.Movies.Include(m=>m.Genres).ToList().Find(m => m.Id == id);
            if (movie != null)
            {
                var newGenres = new List<Genre>();
                foreach(var name in genres)
                {
                    var genre = _context.Genres.ToList().Find(g => g.Name == name);
                    newGenres.Add(genre);
                }
                movie.Genres = newGenres;
                _context.SaveChanges();
                return RedirectToAction("GetMovies");
            }
            return NotFound();
        }

        public async Task<IActionResult> DeleteMovie(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        [HttpPost, ActionName("DeleteMovie")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMovieConfirmed(int id)
        {
            if (_context.Movies == null)
            {
                return Problem("Entity set 'MovieContext.Movies'  is null.");
            }
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GetMovies));
        }

        public async Task<IActionResult> GetGenres()
        {

            return _context.Genres != null ?
                        View(await _context.Genres.ToListAsync()) :
                        Problem("Entity set 'MovieContext.Genres'  is null.");

        }

        public IActionResult CreateGenre()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGenre([Bind("Id,Name")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(genre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(GetGenres));
            }
            return View(genre);
        }

        public async Task<IActionResult> DeleteGenre(int? id)
        {
            if (id == null || _context.Genres == null)
            {
                return NotFound();
            }

            var genre = await _context.Genres
                .FirstOrDefaultAsync(g => g.Id == id);
            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        [HttpPost, ActionName("DeleteGenre")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteGenreConfirmed(int id)
        {
            if (_context.Genres == null)
            {
                return Problem("Entity set 'MovieContext.Genres'  is null.");
            }
            var genre = await _context.Genres.FindAsync(id);
            if (genre != null)
            {
                _context.Genres.Remove(genre);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GetGenres));
        }


        private bool MovieExists(int id)
        {
          return (_context.Movies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

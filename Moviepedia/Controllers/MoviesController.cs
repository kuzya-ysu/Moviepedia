using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Moviepedia.Models;

namespace Moviepedia.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public MoviesController(MovieContext context, UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: Movies
        public async Task<IActionResult> Index(int genreId=0, string language="All")
        {
            var movies = new List<Movie>();
            if (genreId > 0)
            {
                var genre = _context.Genres.Where(g => g.Id == genreId).Include(g=>g.Movies).First();
                movies = _context.Movies.Include(m=>m.Genres).Where(m => m.Genres.Contains(genre)).Include(m => m.Reviews).ToList();
            }
            else
            {
                movies = _context.Movies.Include(m=>m.Reviews).ToList();
            }
            if (language!="All")
            {
                movies = movies.Where(m => m.Language == language).ToList();
            }
            ViewBag.GenresList = GetGenres();
            ViewBag.LanguageList = GetLanguages();
            ViewBag.Searched = false;
            return View(movies);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.Include(m=>m.Genres).Include(m=>m.Reviews)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            if (movie.Reviews == null || movie.Reviews.ToList().Count == 0)
                ViewBag.HasReview = false;
            else
            {
                ViewBag.HasReview = true;
            }
            return View(movie);
        }

        private SelectList GetGenres()
        {
            var genres = _context.Genres.ToList();
            genres.Insert(0, new Genre { Name = "All", Id = 0 });
            var list = new SelectList(genres, "Id", "Name");
            return list;
        }

        private SelectList GetLanguages()
        {
            var movies = _context.Movies.ToList();
            var languages = new HashSet<string>
            {
                "All"
            };
            foreach (var movie in movies)
                languages.Add(movie.Language);
            var list = new SelectList(languages);
            return list;
        }
        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetReviews()
        {
            var userName = HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            var reviews = await _context.Reviews.Where(r=>r.UserId==user.Id).Include(r=>r.Movie).ToListAsync();
            return View(reviews);
        }

        public async Task<ActionResult> AddReview(int? id)
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                if (id == null)
                    return NotFound();

                var movie = await _context.Movies.Include(m => m.Reviews).FirstOrDefaultAsync(m => m.Id == id);

                if (movie == null)
                    return NotFound();

                ViewBag.Movie = movie;

                return View();
            }
            return RedirectToAction("SignIn", "Account");
        }

        [HttpPost]
        public async Task<ActionResult> AddReview([Bind("MovieId,Rating,Text")] Review review)
        {
            if (!ModelState.IsValid)
            {
                var name = HttpContext.User.Identity.Name;
                var user = await _userManager.FindByNameAsync(name);
                review.UserId=user.Id;
                _context.Add(review);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new {id = review.MovieId});
            }
            return View();
        }
        [Authorize(Roles = "user")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = _context.Reviews.ToList().Find(m => m.Id == id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                _context.SaveChanges();
                return RedirectToAction(nameof(GetReviews));
            }
            return NotFound();
        }
        [Authorize(Roles = "user, manager")]
        public IActionResult Search(string searchString)
        {
            var results = new List<Movie>();
            if (!string.IsNullOrEmpty(searchString))
            {
                var rg = new Regex(@"\w+", RegexOptions.IgnoreCase);
                var matches = rg.Matches(searchString.ToLower());
                foreach (var word in matches)
                {
                    int.TryParse(word.ToString(), out int number);
                    var list = _context.Movies.Where(m => m.Director.Contains(word.ToString()) ||
                                                    m.Name.Contains(word.ToString()) || m.Year==number).ToList();
                    results.AddRange(list);
                }
                results = (from item in results select item).Distinct().ToList();
                ViewBag.Searched = true;

                if (results.Count > 0)
                {
                    ViewBag.Msg = "Search results for " + "\"" + searchString + "\":";
                }
                else
                {
                    ViewBag.Msg = "We couldn't find any matches for " +
                        "\"" + searchString + "\"";
                    ViewBag.Msg2 = "Try another search?";
                }
            }
            else
            {
                ViewBag.Searched = false;
                results = _context.Movies.ToList();
            }    
            ViewBag.GenresList = GetGenres();
            ViewBag.LanguageList = GetLanguages();
            return View("Index", results);
        }
    }
}
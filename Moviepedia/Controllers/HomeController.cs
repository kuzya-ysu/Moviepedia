using Microsoft.AspNetCore.Mvc;
using Moviepedia.Models;

namespace Moviepedia.Controllers
{
    public class HomeController : Controller
    {
        private MovieContext _movieContext { get; set; }
        public HomeController(MovieContext movieContext)
        {
            _movieContext = movieContext;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}

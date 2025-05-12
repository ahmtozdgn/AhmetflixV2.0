using Microsoft.AspNetCore.Mvc;

namespace Ahmetflix.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

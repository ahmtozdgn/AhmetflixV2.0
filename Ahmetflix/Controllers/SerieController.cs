using Microsoft.AspNetCore.Mvc;

namespace Ahmetflix.Controllers
{
    public class SerieController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Ahmetflix.Controllers
{
    public class SeriesController : Controller
    {
        public IActionResult SeriesIndex()
        {
            return View();
        }
    }
}
using Microsoft.AspNetCore.Mvc;

namespace MvcChallenge.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace TheWorld.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

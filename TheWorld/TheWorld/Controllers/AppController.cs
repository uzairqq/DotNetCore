using Microsoft.AspNetCore.Mvc;

namespace TheWorld.Controllers
{
    [Route("api/[controller]")]
    public class AppController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}

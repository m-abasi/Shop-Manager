using Microsoft.AspNetCore.Mvc;

namespace MyOwnUIProject.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

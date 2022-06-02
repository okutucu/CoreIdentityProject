using Microsoft.AspNetCore.Mvc;

namespace CoreIdentityProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

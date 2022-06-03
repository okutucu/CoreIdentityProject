using Microsoft.AspNetCore.Mvc;

namespace CoreIdentityProject.Controllers
{
    public class MemberController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TestVersion.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        [Area("Admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            ViewBag.home = "active";
            return View();
        }
        [HttpPost]
        public IActionResult Search(string search)
        {
            return RedirectToAction(search,"Admin");
        }
    }
}

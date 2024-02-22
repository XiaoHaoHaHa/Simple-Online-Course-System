using Microsoft.AspNetCore.Mvc;

namespace FrontEndMVC.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            int x = 1, y = 0;
            ViewBag.Result = x / y;
            return View();
        }
    }
}

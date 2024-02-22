using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;
using CoreLib.Interface;

namespace FrontEndMVC.Controllers
{
    public class LoginController : Controller
    {
        private ILoginCheck _loginCheck;

        public LoginController(ILoginCheck loginCheck)
        {
            _loginCheck = loginCheck;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Course");
        }

        [HttpGet]
        public IActionResult LoginCheck()
        {
            return RedirectToAction("Index", "Course");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> LoginCheck(string email, string password)
        {
            var result = _loginCheck.CheckResult(email, password);
            if (result != null)
            {
                Claim[] claims = new[]
                {
                    new Claim(ClaimTypes.Email, result.Email),
                    new Claim(ClaimTypes.Name, result.Name),
                    new Claim(ClaimTypes.Role, "Student"),
                    new Claim("Phone", result.Phone == null ? "" : result.Phone),
                    new Claim(ClaimTypes.NameIdentifier, result.Id.ToString())
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme); 
                ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(principal));

                return RedirectToAction("Index", "Course");
            }

            //fail case
            TempData["Login"] = "fail";
            return RedirectToAction("Index", "Course");
        }

        public async Task<IActionResult> LogOut()
        { 
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Course");
        }
    }
}

using CoreLib.Interface;
using CoreLib.ClientViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;

namespace FrontEndMVC.Controllers
{
    [Authorize(Roles = "Student")]
    public class ProfileController : Controller
    {
        private IUserService _userService;

        public ProfileController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePwd(ChangePasswordModel input)
        {
            input.Id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            //Check Confirm Password
            if(input.NewPwd != input.ComfirmPwd)
            {
                ViewBag.Error = "The Password is not consistant with the confirm password";
                return View("Index");
            }

            //Check Password Hash
            var result = _userService.ChangePassword(input);
            if(!result)
            {
                ViewBag.Error = "The Password is not correct";
                return View("Index");
            }

            //Update Account
            _userService.UpdateToDataBase(input);

            return RedirectToAction("Logout", "Login");
        }

        public IActionResult ModifyData()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> ChangeProfile(string name, string phone)
        {
            var model = new UserModel()
            {
                Id = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                Name = name,
                Phone = phone
            };

            var result = _userService.UpdateProfileToDataBase(model);
            if (result)
            {
                Claim[] claims = new[]
                {
                    new Claim(ClaimTypes.Email, User.FindFirstValue(ClaimTypes.Email)),
                    new Claim(ClaimTypes.Name, name),
                    new Claim(ClaimTypes.Role, "Student"),
                    new Claim("Phone", phone),
                    new Claim(ClaimTypes.NameIdentifier, User.FindFirstValue(ClaimTypes.NameIdentifier))
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(principal));
                ViewBag.Name = name;
                ViewBag.Phone = phone;
                return View("ModifyData");
            }
            else
            {
                return View("ModifyData");
            }         
        }
    }
}

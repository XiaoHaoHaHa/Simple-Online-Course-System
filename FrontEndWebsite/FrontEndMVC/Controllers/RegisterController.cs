using CoreLib.ClientViewModel;
using CoreLib.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FrontEndMVC.Controllers
{
    public class RegisterController : Controller
    {
        private IUserRegister _userRegister;
        public RegisterController(IUserRegister userRegister)
        {
            _userRegister = userRegister;
        }
        public IActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Register(UserModel user)
        {
            //Not allow pwd is empty
            if (string.IsNullOrEmpty(user.Password))
            {
                ViewBag.ErrorMessage = "The Password Must Have Value";
                return View("Index");
            }
            //passowrd not consistant
            if (user.Password != user.CheckPassword)
            {
                ViewBag.ErrorMessage = "The Password Not Consistant";
                return View("Index");
            }

            //success
            bool result = _userRegister.RegisterProcess(user);
            if (result)
            {
                return RedirectToAction("Index", "Course");
            }

            //fail
            ViewBag.ErrorMessage = "The Email Account Has Been Registered!";
            return View("Index");
        }
    }
}

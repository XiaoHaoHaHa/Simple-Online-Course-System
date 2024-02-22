using CoreLib.ClientViewModel;
using CoreLib.Interface;
using CourseApi.ApiService;
using CourseApi.ClientModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CourseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly JwtHelper _jwt;
        private ILoginCheck _loginCheck;

        public LoginController(JwtHelper jwt, ILoginCheck loginCheck)
        {
            _jwt = jwt;
            _loginCheck = loginCheck;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult LoginCheck(LoginModel user)
        {
            var result = _loginCheck.CheckResult(user.Email, user.Password);
            if (result != null)
            {   
                return Ok(new { token = _jwt.GenToken(result) });
            }

            //fail case
            return Unauthorized(new { message = "Login fail" });
        }
    }
}

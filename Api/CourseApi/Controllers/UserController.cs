using CoreLib.ClientViewModel;
using CoreLib.Interface;
using CourseApi.ClientModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPut("ChangePwd")]
        public IActionResult ChangePwd(ChangePasswordModel input)
        {
            //Check Confirm Password
            if (input.NewPwd != input.ComfirmPwd)
                return BadRequest("Passwor Not Consistent");

            //Check Password Hash
            var result = _userService.ChangePassword(input);
            if (!result)
                return BadRequest("Passwor Not Correct");

            //Update Account
            _userService.UpdateToDataBase(input);
            return Ok("Changed");
        }

        [HttpPut("ChangeInfo")]
        public IActionResult ChangeInfo(ProfileChangeModel input)
        {
            var model = new UserModel() 
            {
                Id = input.Id,
                Name = input.Name,
                Phone = input.Phone
            };

            var result = _userService.UpdateProfileToDataBase(model);
            if (result)
            {
                return Ok("Changed");
            }
            else
            {
                return BadRequest("Not Changed");
            }
        }
    }
}

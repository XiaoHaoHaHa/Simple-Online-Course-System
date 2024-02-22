using CoreLib.ClientViewModel;
using CoreLib.Interface;
using CourseApi.ClientModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CourseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentSelectController : ControllerBase
    {
        private IStudentSelect _studentSelect;

        public StudentSelectController(IStudentSelect studentSelect)
        {
            _studentSelect = studentSelect;
        }

        [HttpPost("StuSelect")]
        public IActionResult StuSelect(ClassSelectModel input)
        {
            var result = _studentSelect.CreateStudentSelect(input.ClassID, input.StudentID);
            if (result)
            {
                return Ok(result);
            }

            //duplicated stuselect
            return BadRequest(result);
        }

        //[HttpGet]
        //public IActionResult Index()
        //{
        //    var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var result = _studentSelect.ShowAllStuSelect(userID);
        //    return View(result.ToList());
        //}

        [HttpDelete("DeleteSelect/{rowSelectID}")]
        public IActionResult DeleteSelect(string rowSelectID)
        {
            var result = _studentSelect.DeletRowData(rowSelectID);
            if (result)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}

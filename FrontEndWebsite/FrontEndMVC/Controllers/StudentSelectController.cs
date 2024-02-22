using CoreLib.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace FrontEndMVC.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentSelectController : Controller
    {
        private IStudentSelect _studentSelect;

        public StudentSelectController(IStudentSelect studentSelect)
        {       
            _studentSelect = studentSelect;
        }

        [HttpPost]
        public IActionResult Index(string courseID)
        {
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            courseID = courseID.Substring(2);
            var result = _studentSelect.CreateStudentSelect(courseID, userID);
            if (result)
            { 
                return RedirectToAction("Index", "Course");
            }

            //duplicated stuselect
            TempData["stuselect"] = "duplicated";
            return RedirectToAction("Index", "Course");
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = _studentSelect.ShowAllStuSelect(userID);
            return View(result.ToList());
        }

        [HttpPost]
        public IActionResult DeleteSelect(string rowSelectID)
        {
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = _studentSelect.DeletRowData(rowSelectID);
            if (result)
                return View("Index", _studentSelect.ShowAllStuSelect(userID).ToList());
            else
                return RedirectToAction("Index", "Course");
        }
    }
}

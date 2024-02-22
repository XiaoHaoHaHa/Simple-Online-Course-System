using Microsoft.AspNetCore.Mvc;
using CoreLib;
using System;
using DataBase;
using System.Collections.Generic;
using System.Linq;
using CoreLib.Interface;
using CoreLib.ClientViewModel;
using X.PagedList;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEndMVC.Controllers
{
    public class CourseController : Controller
    {
        private ICourseRepo _course;
        private const int size = 6;

        public CourseController(ICourseRepo course)
        {
            _course = course;
        }

        public IActionResult Index(int page = 1)
        {
            int currentPage = page < 1 ? 1 : page;
            var classResult = _course.ClassQuery().ToList();
            var result = classResult.ToPagedList(currentPage, size);
            return View(result);
        }

        [HttpPost]
        public IActionResult Index(string TeacherName, string CourseName, int page = 1)
        {
            int currentPage = page < 1 ? 1 : page;
            var query = new CourseQuery()
            { 
                TeacherName = TeacherName,
                CourseName = CourseName
            };

            List<ClassViewModel> result =_course.ClassQuery(query).ToList();
            ViewBag.TeacherName = TeacherName;
            ViewBag.CourseName = CourseName;

            return View("Index", result.ToPagedList(currentPage, size));
        }
    }
}

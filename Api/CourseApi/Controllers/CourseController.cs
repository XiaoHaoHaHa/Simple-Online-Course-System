using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CoreLib.ClientViewModel;
using CoreLib.Interface;
using System.Linq;
using CoreLib;
using DataBase.Tables;

namespace CourseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private ICourseRepo _course;

        public CourseController(ICourseRepo course)
        {
            _course = course;
        }

        /// <summary>
        /// 取得所有課程
        /// </summary>
        /// <remarks>
        /// 這是關於這個`Get`方法，使用方式如下
        /// # title
        /// ```javascript=
        /// var s = "JavaScript syntax highlighting";
        /// alert(s);
        /// function $initHighlight(block, cls)
        /// {
        ///     try
        ///     {
        ///         if (cls.search(/\bno\-highlight\b /) != -1)
        ///             return process(block, true, 0x0F) +
        ///                    ' class=""';
        ///     }
        ///     catch (e)
        ///     {
        ///         /* handle exception */
        ///     }
        /// }
        /// ```
        /// </remarks>
        [HttpGet]
        public IActionResult Get()
        {
            var classResult = _course.ClassQuery().ToList();
            return Ok(classResult);
        }
        
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var classResult = _course.ClassQuery(id);
            if (classResult.Count() == 0)
                return NotFound();
            return Ok(classResult);
        }

        //[Route("api/[controller]/className/teacherName")]
        //public IActionResult Get(string className, string teacherName)
        //{
        //    var query = new CourseQuery() { CourseName = className, TeacherName = teacherName };
        //    var classResult = _course.ClassQuery(query);
        //    if (classResult.Count() == 0)
        //        return NotFound();
        //    return Ok(classResult);
        //}
    }
}

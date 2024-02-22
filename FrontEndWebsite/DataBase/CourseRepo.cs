using System;
using System.Collections.Generic;
using System.Text;
using CoreLib;
using System.Linq;
using DataBase.DB;
using DataBase.Tables;
using CoreLib.Interface;
using CoreLib.ClientViewModel;

namespace DataBase
{
    public class CourseRepo : ICourseRepo
    {
        private TainanNetContext _database;

        public CourseRepo(TainanNetContext database)
        {
            _database = database;
        }
        public IEnumerable<ClassViewModel> ClassQuery()
        {
            var queryList = from classes in _database.Class
                            join course in _database.Course on classes.CourseId equals course.CourseId
                            join teacher in _database.Teacher on classes.TeaId equals teacher.TeaId
                            select new ClassViewModel()
                            {
                                Id = classes.Id.ToString(),
                                CourseName = course.CourseName,
                                TeacherName = teacher.TeaName,
                                Hour = (int)course.CourseHour,
                                Description = classes.Description,
                            };
            return queryList.ToList();
        }

        public IEnumerable<ClassViewModel> ClassQuery(string id)
        {
            var queryList = from classes in _database.Class
                            join course in _database.Course on classes.CourseId equals course.CourseId
                            join teacher in _database.Teacher on classes.TeaId equals teacher.TeaId
                            where classes.Id.ToString() == id
                            select new ClassViewModel()
                            {
                                Id = classes.Id.ToString(),
                                CourseName = course.CourseName,
                                TeacherName = teacher.TeaName,
                                Hour = (int)course.CourseHour,
                                Description = classes.Description,
                            };
            return queryList.ToList();
        }

        public IEnumerable<ClassViewModel> ClassQuery(CourseQuery query)
        {
            var queryList = from classes in _database.Class
                            join course in _database.Course on classes.CourseId equals course.CourseId
                            join teacher in _database.Teacher on classes.TeaId equals teacher.TeaId
                            select new ClassViewModel()
                            {
                                //CourseID 需要改成 ClassID
                                Id = classes.Id.ToString(),
                                CourseName = course.CourseName,
                                TeacherName = teacher.TeaName,
                                Hour = (int)course.CourseHour,
                                Description = classes.Description,
                            };
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.CourseName))
                {
                    queryList = queryList.Where(o => o.CourseName.Contains(query.CourseName));
                }

                if (!string.IsNullOrEmpty(query.TeacherName))
                {
                    queryList = queryList.Where(o => o.TeacherName.Contains(query.TeacherName));
                }
            }
                  
            return queryList.ToList();
        }
    }
}

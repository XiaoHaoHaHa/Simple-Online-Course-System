using CoreLib.ClientViewModel;
using CoreLib.Interface;
using DataBase.DB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DataBase.Tables;

namespace DataBase
{
    public class StuSelectRepo : IStuSelectRepo
    {
        private TainanNetContext _database;

        public StuSelectRepo(TainanNetContext database)
        {
            _database = database;
        }

        public IEnumerable<StudentSelectModel> StuSelectQuery(string studentID)
        {
            var result = from stuSelect in _database.StuSelect join 
                         classes in _database.Class on stuSelect.ClassId equals classes.Id join
                         course in _database.Course on classes.CourseId equals course.CourseId join
                         teacher in _database.Teacher on classes.TeaId equals teacher.TeaId
                         where stuSelect.StuId.ToString() == studentID
                         select new StudentSelectModel() { 
                            Id = stuSelect.Id,
                            CourseName = course.CourseName,
                            TeacherName = teacher.TeaName,
                            Hour = (int)course.CourseHour,
                            sDate = classes.SDate.ToString(),
                            eDate = classes.EDate.ToString(),
                            Loaction = classes.Location
                         };
            return result.ToList();
        }

        public StudentSelectModel StudentSelectQuery(string courseID, string studentID)
        {
            var result = _database.StuSelect.Where(i => i.ClassId.ToString() == courseID && i.StuId.ToString() == studentID).FirstOrDefault();
            if (result == null)
            { 
                return null;
            }
            return new StudentSelectModel() { ClassID = result.ClassId, StudentID = result.StuId };
        }

        public void UpdateStuSelectToDatabase(string courseID, string studentID)
        {
            StuSelect entity = new StuSelect()
            {
                StuId = Guid.Parse(studentID),
                ClassId = Guid.Parse(courseID),
            };

            _database.StuSelect.Add(entity);
            _database.SaveChanges();
        }

        public bool DeleteStuSelect(string stuSelectID)
        {
            var result = _database.StuSelect.Where(i => i.Id.ToString() == stuSelectID).FirstOrDefault();
            if (result == null)
                return false;
            _database.StuSelect.Remove(result);
            _database.SaveChanges();
            return true;
        }
    }
}

using CoreLib.ClientViewModel;
using CoreLib.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreLib
{
    public class StudentSelect : IStudentSelect
    {
        private IStuSelectRepo _stuSelectRepo;

        public StudentSelect(IStuSelectRepo stuSelectRepo)
        {
            _stuSelectRepo = stuSelectRepo;
        }
        public bool CreateStudentSelect(string courseID, string studentID)
        {
            var result = IfStudentSelected(courseID, studentID);
            if (result == false)
            {
                _stuSelectRepo.UpdateStuSelectToDatabase(courseID, studentID);
                return true;
            }
            return false;
        }

        public bool DeletRowData(string rowSelectID)
        {
            var result = _stuSelectRepo.DeleteStuSelect(rowSelectID);
            return result;
        }

        public bool IfStudentSelected(string courseID, string studentID)
        {
            var result = _stuSelectRepo.StudentSelectQuery(courseID, studentID);
            if(result != null)
                return true;
            return false;
        }

        public IEnumerable<StudentSelectModel> ShowAllStuSelect(string studentID)
        {
            var result = _stuSelectRepo.StuSelectQuery(studentID);
            return result;
        }
    }
}

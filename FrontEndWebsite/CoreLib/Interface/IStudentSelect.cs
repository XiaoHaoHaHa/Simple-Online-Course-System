using CoreLib.ClientViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLib.Interface
{
    public interface IStudentSelect
    {
        bool IfStudentSelected(string courseID, string studentID);
        bool CreateStudentSelect(string courseID, string studentID);
        IEnumerable<StudentSelectModel> ShowAllStuSelect(string studentID);
        bool DeletRowData(string rowSelectID);
        //Show Studentselect result 
    }
}

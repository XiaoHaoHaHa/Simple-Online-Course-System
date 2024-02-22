using CoreLib.ClientViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLib.Interface
{
    public interface IStuSelectRepo
    {
        StudentSelectModel StudentSelectQuery(string courseID, string studentID);
        void UpdateStuSelectToDatabase(string courseID, string studentID);
        IEnumerable<StudentSelectModel> StuSelectQuery(string studentID);
        bool DeleteStuSelect(string stuSelectID);
    }
}

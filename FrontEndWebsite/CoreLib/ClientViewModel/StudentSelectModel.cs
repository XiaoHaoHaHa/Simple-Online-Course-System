using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLib.ClientViewModel
{
    public class StudentSelectModel
    {
        public Guid Id { get; set; }
        public Guid StudentID { get; set; }
        public Guid ClassID { get; set; }
        public string TeacherName { get; set; }
        public string CourseName { get; set; }
        public int Hour { get; set; }
        public string sDate { get; set; }
        public string eDate { get; set; }
        public string Loaction { get; set; }
    }
}

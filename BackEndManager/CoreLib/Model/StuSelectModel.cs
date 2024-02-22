using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Model
{
    public class StuSelectModel
    {
        public string CourseName { get; set; }
        public string TeacherName { get; set; }
        public DateTime SDate { get; set; }
        public DateTime EDate { get; set; }
        public int Hour { get; set; }
        public string Location { get; set; }
    }
}

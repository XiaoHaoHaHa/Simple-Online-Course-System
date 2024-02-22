using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Model
{
    public class ClassModel
    {
        public string Id { get; set; }
        public string CourseID { get; set; }
        public string TeacherID { get; set; }
        public string CourseName { get; set; }
        public string TeacherName { get; set; }
        public DateTime SDate { get; set; }
        public DateTime EDate { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }

        public ClassModel()
        {
            this.Id = Guid.NewGuid().ToString();
            this.SDate = DateTime.Now;
            this.EDate = DateTime.Now;
        }
    }
}

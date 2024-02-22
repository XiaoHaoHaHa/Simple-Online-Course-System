using System;
using System.Collections.Generic;
using System.Text;
using CoreLib.ClientViewModel;

namespace CoreLib.Interface
{
    public interface ICourseRepo
    {
        IEnumerable<ClassViewModel> ClassQuery();

        IEnumerable<ClassViewModel> ClassQuery(CourseQuery query);
        IEnumerable<ClassViewModel> ClassQuery(string id);
    }
}

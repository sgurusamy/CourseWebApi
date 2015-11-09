using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.BusinessLogic
{
    public interface ICourse
    {
        string OrderCourses(List<string> courseList);
    }
}

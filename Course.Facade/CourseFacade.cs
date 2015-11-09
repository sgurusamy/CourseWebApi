using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL= Course.BusinessLogic;

namespace Course.Facade
{
    public class CourseFacade : ICourseFacade
    {
        public string OrderCourses(List<string> courseList)
        {
            BL.Course course = new BL.Course();
            return course.OrderCourses(courseList);            
        }
    }
}

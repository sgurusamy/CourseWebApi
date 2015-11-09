using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Persistence
{
    public interface ICourseRepository
    {
        void AddCourse(string courseName);
        void AddCoursewithPreRequiste(string courseName, string PreRequisite);
        int getCourseID(string courseName);
        List<string> getAllCourses();
    }
}

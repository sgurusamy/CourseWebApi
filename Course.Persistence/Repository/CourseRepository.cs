using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DE = Course.DataEntities;
namespace Course.Persistence
{
    public class CourseRepository : ICourseRepository
    {

        public void AddCourse(string courseName)
        {
            using (var context = new CourseDbContext())
            {
                DE.Course course = new DE.Course();
                course.Description  = courseName;
                context.Courses.Add(course);
                context.SaveChanges();
            }
        }

        public void AddCoursewithPreRequiste(string courseName, string PreRequisite)
        {
            using (var context = new CourseDbContext())
            {
                DE.PrerequisiteCourse preReq = new DE.PrerequisiteCourse();
                preReq.Id_Course = this.getCourseID(courseName);
                int preReqId = this.getCourseID(PreRequisite);
                if (preReqId > 0)
                {
                    preReq.Id_Prereq_Course = preReqId;
                }
                context.PrerequisiteCourses.Add(preReq);
                context.SaveChanges();
            }
        }

        public int getCourseID(string courseName)
        {
            int Id = 0;
            using (var context = new CourseDbContext())
            {
                DE.Course course =  context.Courses.Where(a => a.Description == courseName).FirstOrDefault<DE.Course>();
                if (course != null)
                {
                    Id = course.CourseID;
                }
            }
            return Id;
        }

        public List<string> getAllCourses()
        {
            using (var context = new CourseDbContext())
            {
                return context.Courses.Select(c => c.Description).ToList<string>();
            }
        }
    }
}

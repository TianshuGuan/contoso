using ContosoDAL.Repositories.Common;
using ContosoDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoDAL.Repositories
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository() : base()
        {
        }

        public void Enroll(Student student, Course course)
        {
            _context.Enrollments.Add(new Enrollment(student.Id, course.Id));
        }
    }

    public interface ICourseRepository : IRepository<Course>
    {
        void Enroll(Student student, Course course);
    }
}

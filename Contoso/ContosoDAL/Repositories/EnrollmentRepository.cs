using ContosoDAL.Repositories.Common;
using ContosoDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoDAL.Repositories
{
    public class EnrollmentRepository:GenericRepository<Enrollment>,IEnrollmentRepository
    {
        public EnrollmentRepository() : base()
        {
        }

        public Enrollment GetByCourseAndStudent(int courseId, string studentId)
        {
            return Get(e => e.CourseId == courseId && e.StudentId == studentId);
        }
    }

    public interface IEnrollmentRepository : IRepository<Enrollment>
    {
        Enrollment GetByCourseAndStudent(int courseId, string studentId);
    }
}

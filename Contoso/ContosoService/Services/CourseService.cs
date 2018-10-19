using ContosoDAL.Repositories;
using ContosoDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ContosoService.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public void AddCourse(Course course)
        {
            using (var transaction = new TransactionScope())
            {
                _courseRepository.Add(course);
                _courseRepository.SaveChanges();
                transaction.Complete();
            }
        }

        public void DeleteCourse(Course course)
        {
            using (var transaction = new TransactionScope())
            {
                _courseRepository.Delete(course);
                _courseRepository.SaveChanges();
                transaction.Complete();
            }
        }

        public void Enroll(Student student, Course course)
        {
            using (var transaction = new TransactionScope())
            {
                _courseRepository.Enroll(student, course);
                _courseRepository.SaveChanges();
                transaction.Complete();
            }

        }

        public IEnumerable<Course> GetAllCourse()
        {
            return _courseRepository.GetAll();
        }

        public IEnumerable<Course> GetAllCourseByDepartment(int departmentId)
        {
            return _courseRepository.GetMany(c => c.DepartmentId == departmentId);
        }

        public IEnumerable<Course> GetAllCourseByInstructor(Instructor insturctor)
        {
            return _courseRepository.FindByInclude(c => c.Instructors.Contains(insturctor), c => c.Instructors);
        }

        public void UpdateCourse(Course course)
        {
            Course target = _courseRepository.GetById(course.Id);
            using (var transaction = new TransactionScope())
            {
                
                _courseRepository.Update(target);
                if (course.Title != target.Title)
                {
                    target.Title = course.Title;
                }
                if (course.Credit != target.Credit)
                {
                    target.Credit = course.Credit;
                }
                _courseRepository.SaveChanges();
                transaction.Complete();
            }
        }
    }

    public interface ICourseService: IService
    {
        void Enroll(Student student, Course course);
        IEnumerable<Course> GetAllCourse();
        IEnumerable<Course> GetAllCourseByDepartment(int departmentId);
        IEnumerable<Course> GetAllCourseByInstructor(Instructor instructor);
        void AddCourse(Course course);
        void UpdateCourse(Course course);
        void DeleteCourse(Course course);
    }
}

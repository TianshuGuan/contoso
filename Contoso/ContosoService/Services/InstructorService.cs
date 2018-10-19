using ContosoDAL.Repositories;
using ContosoDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static ContosoDomain.Models.Enrollment;

namespace ContosoService.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly IInstructorRepository _instructorRepository;
        public InstructorService(IInstructorRepository instructorRepository)
        {
            _instructorRepository = instructorRepository;
        }
        public IEnumerable<Instructor> GetAllInstructors()
        {
            return _instructorRepository.GetAll();
        }

        public Instructor GetInstructorById(string id)
        {
            return _instructorRepository.GetById(id);
        }
        //get instructors by last name
        public IEnumerable<Instructor> GetInstructorByName(string FirstName, string LastName)
        {
            return _instructorRepository.GetMany(i => i.LastName == LastName && i.FirstName == FirstName);
        }

        public void CreateInstructor(Instructor instructor)
        {
            using (var transaction = new TransactionScope())
            {
                _instructorRepository.Add(instructor);
                _instructorRepository.SaveChanges();
                transaction.Complete();
            }
        }

        public void UpdateInstructor(Instructor instructor)
        {
            Instructor target = _instructorRepository.GetById(instructor.Id);
            using (var transaction = new TransactionScope())
            {
                _instructorRepository.Update(target);
                if (!target.FirstName.Equals(instructor.FirstName))
                {
                    target.FirstName = instructor.FirstName;
                }
                if (!target.LastName.Equals(instructor.LastName))
                {
                    target.LastName = instructor.LastName;
                }
                if(!target.AddressLine1.Equals(instructor.AddressLine1))
                {
                    target.AddressLine1 = instructor.AddressLine1;
                }
                if(!target.AddressLine2.Equals(instructor.AddressLine2))
                {
                    target.AddressLine2 = instructor.AddressLine2;
                }
                if (!target.City.Equals(instructor.City))
                {
                    target.City = instructor.City;
                }
                if (!target.MiddleName.Equals(instructor.MiddleName))
                {
                    target.MiddleName = instructor.MiddleName;
                }
                if (!target.PhoneNumber.Equals(instructor.PhoneNumber))
                {
                    target.PhoneNumber = instructor.PhoneNumber;
                }
                _instructorRepository.SaveChanges();
                transaction.Complete();
            }
        }

        public IEnumerable<Instructor> GetInstructorByDepartment(int departmentId)
        {
            return _instructorRepository.FindByInclude(i => i.Department.Id == departmentId, i => i.Department);
        }

        public IEnumerable<Instructor> GetInstructorByCourse(Course course)
        {

            return _instructorRepository.FindByInclude(i => i.Courses.Contains(course) , i => i.Courses);
        }

        public void changeDepartment(Instructor instructor, int DepartmentId)
        {
            Instructor target = _instructorRepository.GetById(instructor.Id);
            using (var transaction = new TransactionScope())
            {
                _instructorRepository.Update(target);
                target.DepartmentId = DepartmentId;
                _instructorRepository.SaveChanges();
                transaction.Complete();
            }
        }

    }

    public interface IInstructorService: IService
    {
        IEnumerable<Instructor> GetAllInstructors();
        Instructor GetInstructorById(string id);
        IEnumerable<Instructor> GetInstructorByName(string FirstName, string LastName);
        IEnumerable<Instructor> GetInstructorByDepartment(int departmentId);
        IEnumerable<Instructor> GetInstructorByCourse(Course course);
        void CreateInstructor(Instructor instructor);
        void UpdateInstructor(Instructor instructor);
        void changeDepartment(Instructor instructor, int DepartmentId);

    }
}

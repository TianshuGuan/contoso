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
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _studentRepository.GetAll();
        }

        public Student GetStudentById(string id)
        {
            return _studentRepository.GetById(id);
        }

        public IEnumerable<Student> GetStudentByName(string name)
        {
            return _studentRepository.GetMany(s => s.LastName.Contains(name) || s.FirstName.Contains(name)).ToList();
        }

        public void CreateStudent(Student student)
        {
            using (var transaction = new TransactionScope())
            {
                _studentRepository.Add(student);
                _studentRepository.SaveChanges();
                transaction.Complete();
            }
        }

        public void UpdateStudent(Student student)
        {
            Student target = _studentRepository.GetById(student.Id);
            using (var transaction = new TransactionScope())
            {
                _studentRepository.Update(student);
                if (!target.FirstName.Equals(student.FirstName))
                {
                    target.FirstName = student.FirstName;
                }
                if (!target.LastName.Equals(student.LastName))
                {
                    target.LastName = student.LastName;
                }
                if (!target.AddressLine1.Equals(student.AddressLine1))
                {
                    target.AddressLine1 = student.AddressLine1;
                }
                if (!target.AddressLine2.Equals(student.AddressLine2))
                {
                    target.AddressLine2 = student.AddressLine2;
                }
                if (!target.City.Equals(student.City))
                {
                    target.City = student.City;
                }
                if (!target.MiddleName.Equals(student.MiddleName))
                {
                    target.MiddleName = student.MiddleName;
                }
                if (!target.PhoneNumber.Equals(student.PhoneNumber))
                {
                    target.PhoneNumber = student.PhoneNumber;
                }
                _studentRepository.SaveChanges();
                transaction.Complete();
            }
        }

        public void DeleteStudent(Student student)
        {
            using (var transaction = new TransactionScope())
            {
                _studentRepository.Delete(student);
                _studentRepository.SaveChanges();
                transaction.Complete();
            }
        }
    }

    public interface IStudentService: IService
    {
        IEnumerable<Student> GetAllStudents();
        Student GetStudentById(string id);
        IEnumerable<Student> GetStudentByName(string name);
        void CreateStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudent(Student student);
    }
}

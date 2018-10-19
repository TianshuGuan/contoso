using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using ContosoDAL.Repositories;
using ContosoDomain.Models;
using static ContosoDomain.Models.Enrollment;

namespace ContosoService.Services
{
    public class RegisterService : IRegisterService
    {
        protected readonly IEnrollmentRepository _enrollmentRepository;
        public RegisterService(IEnrollmentRepository repo)
        {
            _enrollmentRepository = repo;
        }
        public void Grader(int course, string student, Grade grade)
        {
            Enrollment target = _enrollmentRepository.GetByCourseAndStudent(course, student);
            using(var transaction = new TransactionScope())
            {
                _enrollmentRepository.Update(target);
                target.Grades = grade;
                _enrollmentRepository.SaveChanges();
                transaction.Complete();
            }
        }
    }

    public interface IRegisterService: IService
    {
        void Grader(int course, string student, Grade grade);
    }
}

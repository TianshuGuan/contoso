using ContosoDAL.Repositories.Common;
using ContosoDomain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ContosoDAL.Repositories
{
    public class StudentRepository : UserRepository<Student>, IStudentRepository
    {

        public StudentRepository():base()
        {
        }
    }

    public interface IStudentRepository : IUserRepository<Student>
    {

    }
}

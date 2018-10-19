using ContosoDAL.Repositories.Common;
using ContosoDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoDAL.Repositories
{
    public class InstructorRepository:UserRepository<Instructor>,IInstructorRepository
    {
        public InstructorRepository() : base()
        {

        }
    }

    public interface IInstructorRepository : IUserRepository<Instructor>
    {

    }
}

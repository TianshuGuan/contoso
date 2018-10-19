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
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public void Create(Department department)
        {
            using (var transaction = new TransactionScope())
            {
                _departmentRepository.Add(department);
                _departmentRepository.SaveChanges();
                transaction.Complete();
            }
        }

        public void Delete(int Id)
        {
            using(var transaction = new TransactionScope())
            {
                var department = _departmentRepository.GetById(Id);
                _departmentRepository.Delete(department);
                _departmentRepository.SaveChanges();
                transaction.Complete();
            }
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            return _departmentRepository.GetAll();
        }

        public IEnumerable<Department> GetAllDepartmentsIncludeCourses()
        {
            return _departmentRepository.GetAllDepartmentsIncludeCourses();
        }

        public Department GetByName(string name)
        {
            return _departmentRepository.Get(d => d.Name == name);
        }

        public void Update(Department department)
        {
            Department target = _departmentRepository.GetById(department.Id);
            using (var transaction = new TransactionScope())
            {
                _departmentRepository.Update(target);
                if (!target.Name.Equals(department.Name))
                {
                    target.Name = department.Name;
                }
                if (!target.StartDate.Equals(department.StartDate))
                {
                    target.StartDate = department.StartDate;
                }
                if(target.Budget != department.Budget)
                {
                    target.Budget = department.Budget;
                }
                _departmentRepository.SaveChanges();
                transaction.Complete();
            }
        }
    }

    public interface IDepartmentService: IService
    {
        IEnumerable<Department> GetAllDepartments();
        IEnumerable<Department> GetAllDepartmentsIncludeCourses();
        Department GetByName(string name);
        void Delete(int Id);
        void Create(Department department);
        void Update(Department department);
    }
}

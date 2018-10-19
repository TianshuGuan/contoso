using ContosoDomain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Contoso.ViewModels
{
    public class BaseDepartmentDTO
    {
        [MaxLength(50)]
        public string Name { get; set; }

        public BaseDepartmentDTO()
        {

        }
        public BaseDepartmentDTO(Department department)
        {
            if(department!=null) this.Name = department.Name;
        }

        public virtual Department toEntity()
        {
            return new Department { Name = this.Name };

        }
    }
    public class DepartmentDTO:BaseDepartmentDTO
    {
        public int Budget { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        public DepartmentDTO(Department department) : base(department)
        {
            if (department != null) this.Budget = department.Budget;
            if (department != null) this.StartDate = department.StartDate;
        }
        public DepartmentDTO()
        {

        }
        public override Department toEntity()
        {
            Department entity = base.toEntity();
            entity.Budget = this.Budget;
            entity.StartDate = this.StartDate;
            return entity;
        }
    }

    public class DepartmentEditDTO : DepartmentDTO
    {
        
        public int Id { get; set; }

        public DepartmentEditDTO(Department department) : base(department)
        {
            if (department != null) this.Id = department.Id;
        }
        public DepartmentEditDTO()
        {

        }
        public override Department toEntity()
        {
            Department entity = base.toEntity();
            entity.Id = this.Id;
            return entity;
        }
    }
}
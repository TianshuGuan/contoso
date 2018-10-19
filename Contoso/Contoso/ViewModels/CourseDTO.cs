using ContosoDomain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Contoso.ViewModels
{
    public class BaseCourseDTO
    {
        [MaxLength(50)]
        public string Title { get; set; }

        public BaseCourseDTO(Course course)
        {
            this.Title = course.Title;
        }

        public virtual Course toEntity(int departmentId)
        {
            return new Course
            {
                Title = this.Title
            };
        }
    }

    public class CourseDTO:BaseCourseDTO
    {
        public int Credit { get; set; }
        [Display(Name = "Department")]
        public string DepartmentName { get; set; }

        public CourseDTO(Course course) : base(course)
        {
            this.Credit = course.Credit;
            this.DepartmentName = course.Department.Name;
        }

        public override Course toEntity(int departmentID)
        {
            Course entity = base.toEntity(departmentID);
            entity.Credit = this.Credit;
            entity.DepartmentId = departmentID;
            return entity;
        }
    }

    public class CourseEditDTO:CourseDTO
    {
        public int Id { get; set; }

        public CourseEditDTO(Course course) : base(course)
        {
            this.Id = course.Id;
        }

        public override Course toEntity(int departmentID)
        {
            Course entity = base.toEntity(departmentID);
            entity.Id = this.Id;
            return entity;
        }

    }
}
using ContosoDomain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Contoso.ViewModels
{
    public class BaseUserDTO
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        public BaseUserDTO(ApplicationUser user)
        {
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
        }

        public virtual ApplicationUser toEntity()
        {
            return new ApplicationUser
            {
                FirstName = this.FirstName,
                LastName = this.LastName
            };
        }
    }
    public class UserDTO:BaseUserDTO
    {
        [MaxLength(50)]
        public string MiddleName { get; set; }
        [MaxLength(150)]
        public string AddressLine1 { get; set; }
        [MaxLength(150)]
        public string AddressLine2 { get; set; }

        public int UnitOrAppartmentNumber { get; set; }
        [MaxLength(50)]
        public string City { get; set; }
        [MaxLength(20)]
        public string State { get; set; }
        [MaxLength(9)]
        //[RegularExpression(@"^(?!0{5})(\d{5})(?!-?0{4})(-?\d{4})?$")]
        public string ZipCode { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public UserDTO(ApplicationUser user) : base(user)
        {
            this.AddressLine1 = user.AddressLine1;
            this.AddressLine2 = user.AddressLine2;
            this.City = user.City;
            this.DateOfBirth = user.DateOfBirth;
            this.Email = user.Email;
            this.State = user.State;
            this.MiddleName = user.MiddleName;
            this.ZipCode = user.ZipCode;
            this.UnitOrAppartmentNumber = user.UnitOrAppartmentNumber;
            this.PhoneNumber = user.PhoneNumber;
        }

        public override ApplicationUser toEntity()
        {
            ApplicationUser entity = base.toEntity();
            entity.MiddleName = this.MiddleName;
            entity.AddressLine1 = this.AddressLine1;
            entity.AddressLine2 = this.AddressLine2;
            entity.City = this.City;
            entity.DateOfBirth = this.DateOfBirth;
            entity.Email = this.Email;
            entity.UnitOrAppartmentNumber = this.UnitOrAppartmentNumber;
            entity.State = this.State;
            entity.ZipCode = this.ZipCode;
            entity.PhoneNumber = this.PhoneNumber;
            return entity;
        }
    }

    public class StudentDTO : UserDTO
    {
        [DataType(DataType.DateTime)]
        public DateTime EnrollmentDate { get; set; }

        public StudentDTO(Student student):base(student)
        {
            this.EnrollmentDate = student.EnrollmentDate;
        }

        public Student toStudent()
        {
            return new Student
            {
                FirstName = this.FirstName,
                LastName = this.LastName,
                MiddleName = this.MiddleName,
                AddressLine1 = this.AddressLine1,
                AddressLine2 = this.AddressLine2,
                City = this.City,
                DateOfBirth = this.DateOfBirth,
                Email = this.Email,
                UnitOrAppartmentNumber = this.UnitOrAppartmentNumber,
                State = this.State,
                ZipCode = this.ZipCode,
                PhoneNumber = this.PhoneNumber,
                EnrollmentDate = this.EnrollmentDate
            };
        }
    }

    public class InstructorDTO : UserDTO
    {
        [DataType(DataType.DateTime)]
        public DateTime HiredDate { get; set; }
        [Display(Name = "Department")]
        public string DepartmentName { get; set; }

        public InstructorDTO(Instructor user) : base(user)
        {
            this.HiredDate = user.HiredDate;
            this.DepartmentName = user.Department.Name;
        }

        public Instructor toInstructor(int departmentId)
        {
            return new Instructor
            {
                FirstName = this.FirstName,
                LastName = this.LastName,
                MiddleName = this.MiddleName,
                AddressLine1 = this.AddressLine1,
                AddressLine2 = this.AddressLine2,
                City = this.City,
                DateOfBirth = this.DateOfBirth,
                Email = this.Email,
                UnitOrAppartmentNumber = this.UnitOrAppartmentNumber,
                State = this.State,
                ZipCode = this.ZipCode,
                PhoneNumber = this.PhoneNumber,
                HiredDate = this.HiredDate,
                DepartmentId = departmentId
            };
        }
    }
}
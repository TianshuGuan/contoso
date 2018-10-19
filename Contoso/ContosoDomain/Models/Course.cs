using ContosoDomain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoDomain.Models
{
    public class Course:AuditableEntity
    {
        [MaxLength(50)]
        public string Title { get; set; }

        public int Credit { get; set; }
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }

        public virtual ICollection<Instructor> Instructors { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}

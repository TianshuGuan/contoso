using ContosoDomain.Models;
using ContosoDomain.Models.Common;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoDAL
{
    public class ContosoDBContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Instructor> Instructors { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Enrollment> Enrollments { get; set; }
        public virtual DbSet<OfficeAssignment> OfficeAssignments { get; set; }

        public ContosoDBContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ContosoDBContext Create()
        {
            return new ContosoDBContext();
        }

        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is AuditableEntity
                            &&
                            (x.State == System.Data.Entity.EntityState.Added ||
                             x.State == System.Data.Entity.EntityState.Modified));

            foreach (var entry in modifiedEntries)
            {
                AuditableEntity entity = entry.Entity as AuditableEntity;
                if (entity != null)
                {
                    //string identityName = Thread.CurrentPrincipal.Identity.Name;
                    string identityName = "admin";
                    DateTime now = DateTime.Now;

                    if (entry.State == System.Data.Entity.EntityState.Added)
                    {
                        entity.CreatedBy = identityName;
                        entity.CreatedDateTime = now;
                    }
                    else
                    {
                        base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                        base.Entry(entity).Property(x => x.CreatedDateTime).IsModified = false;
                    }

                    entity.UpdatedBy = identityName;
                    entity.UpdatedDateTime = now;
                }
            }

            return base.SaveChanges();
        }
    }
}

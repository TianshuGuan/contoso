using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoDomain.Models.Common
{
    public class AuditableEntity:BaseEntity
    {
        [DataType(DataType.DateTime)]
        public DateTime? CreatedDateTime { get; set; }
        [MaxLength(50)]
        public string CreatedBy { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? UpdatedDateTime { get; set; }
        [MaxLength(50)]
        public string UpdatedBy { get; set; }
    }
}

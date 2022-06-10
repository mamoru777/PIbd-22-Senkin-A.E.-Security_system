using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SecuritySystemDatabaseImplement.Models
{
    class SecureComponent
    {
        public int Id { get; set; }
        public int SecureId { get; set; }
        public int ComponentId { get; set; }
        [Required]
        public int Count { get; set; }
        public virtual Component Component { get; set; }
        public virtual Secure Secure { get; set; }
    }
}

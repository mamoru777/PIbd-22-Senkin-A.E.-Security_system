using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using SecuritySystemContracts.Enums;

namespace SecuritySystemDatabaseImplement.Models
{
    class Order
    {
        public int Id { get; set; }
        public int SecureId { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public decimal Sum { get; set; }
        [Required]
        public OrderStatus Status { get; set; }
        [Required]
        public DateTime DateCreate { get; set; }
        public DateTime? DateImplement { get; set; }
        public virtual Secure Secure { get; set; }
    }
}

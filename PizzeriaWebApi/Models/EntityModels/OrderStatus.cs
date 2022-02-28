using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.EntityModels
{
    [Table("OrderStatus")]
    public class OrderStatus
    {
        public OrderStatus()
        {
            Orders = new HashSet<Order>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(32)]
        public string Name { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [InverseProperty("OrderStatus")]
        public ICollection<Order> Orders { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{
    [Table("OrderStatus")]
    public class OrderStatus
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        [InverseProperty("OrderStatus")]
        public ICollection<Order> Orders { get; set; }

        [Required]
        [InverseProperty("OrderStatus")]
        public ICollection<OrderHistory> OrderHistories { get; set; }
    }
}
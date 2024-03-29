using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models.Entity
{
    [Table("Delivery")]
    public class Delivery
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName ="decimal(16, 2)")]
        public decimal Price { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public int IdOrder { get; set;}

        [Required]
        [ForeignKey("IdOrder")]
        public Order Order { get; set; }

        [Required]
        public int IdDeliveryAddress { get; set; }

        [Required]
        [ForeignKey("IdDeliveryAddress")]
        public DeliveryAddress DeliveryAddress{ get; set; }
    }
}
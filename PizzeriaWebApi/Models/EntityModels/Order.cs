using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.EntityModels
{
    [Table("Order")]
    public class Order
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; }

        [Required]
        public TimeSpan RegistrationTime { get; set; }

        [Required]
        public DateTime ReceivingDate { get; set; }

        [Required]
        public TimeSpan ReceivingTime { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public int IdUser { get; set; }

        [Required]
        [ForeignKey("IdUser")]
        public User User { get; set; }

        [Required]
        public int IdBuilding { get; set; }

        [Required]
        [ForeignKey("IdBuilding")]
        public Building Building { get; set; }

        [Required]
        public int IdReceivingMethod { get; set; }

        [Required]
        [ForeignKey("IdReceivingMethod")]
        public ReceivingMethod ReceivingMethod { get; set; }

        [Required]
        public int IdStatus { get; set; }

        [Required]
        [ForeignKey("IdStatus")]
        public OrderStatus OrderStatus { get; set; }

        [Required]
        [InverseProperty("Order")]
        public ICollection<OrderHistory> OrderHistory { get; set; }

        [Required]
        [InverseProperty("OrderIncludeProductVariety")]
        public ICollection<OrderIncludeProductVariety> OrderIncludeProductVariety { get; set; }

        [Required]
        [InverseProperty("Delivery")]
        public ICollection<Delivery> Delivery { get; set; }
    }
}
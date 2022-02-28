using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.EntityModels
{
    [Table("Order")]
    public class Order
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public DateTime RegistrationDate { get; set; }
        public TimeSpan RegistrationTime { get; set; }
        public DateTime ReceivingDate { get; set; }
        public TimeSpan ReceivingTime { get; set; }
        public int IdBuilding { get; set; }
        public int IdReceivingMethod { get; set; }
        public decimal TotalPrice { get; set; }
        public int IdStatus { get; set; }
        public bool IsDeleted { get; set; }

        [Required]
        [ForeignKey("IdReceivingMethod")]
        public ReceivingMethod ReceivingMethod { get; set; }

        [Required]
        [ForeignKey("IdStatus")]
        public OrderStatus OrderStatus { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.EntityModels
{
    [Table("OrderHistory")]
    public class OrderHistory
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan Time { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public int IdOrder { get; set; }

        [Required]
        [ForeignKey("IdOrder")]
        public Order Order { get; set; }

        [Required]
        public int IdStatus { get; set; }

        [Required]
        [ForeignKey("IdStatus")]
        public OrderStatus OrderStatus { get; set; }

        [Required]
        public int IdOperation { get; set; }

        [Required]
        [ForeignKey("IdOperation")]
        public Operation Operation { get; set; }
    }
}
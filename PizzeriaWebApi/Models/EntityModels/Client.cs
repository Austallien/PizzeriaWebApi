using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.EntityModels
{
    [Table("Client")]
    public class Client
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int IdUser { get; set; }

        [Required]
        public double TotalOrderPrice { get; set; }

        [Required]
        public int IdDiscount { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [ForeignKey("IdUser")]
        public User User { get; set; }

        [ForeignKey("IdDiscount")]
        public Discount Discount { get; set; }
    }
}
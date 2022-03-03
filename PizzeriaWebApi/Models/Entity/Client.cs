using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{
    [Table("Client")]
    public class Client
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public int IdUser { get; set; }

        [Required]
        [ForeignKey("IdUser")]
        public User User { get; set; }

        [Required]
        public int IdDiscount { get; set; }

        [Required]
        [ForeignKey("IdDiscount")]
        public Discount Discount { get; set; }
    }
}
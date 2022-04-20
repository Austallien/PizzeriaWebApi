using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models.Entity
{
    [Table("Discount")]
    public class Discount
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "decimal(3, 2)")]
        public decimal Value { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(7, 2)")]
        public decimal Threshold { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        [InverseProperty("Discount")]
        public ICollection<Client> Clients { get; set; }
    }
}

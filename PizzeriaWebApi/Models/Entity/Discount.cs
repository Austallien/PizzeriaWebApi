using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{
    [Table("Discount")]
    public class Discount
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Value { get; set; }
        
        [Required]
        public decimal Threshold { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        [InverseProperty("Discount")]
        public ICollection<Client> Clients { get; set; }
    }
}

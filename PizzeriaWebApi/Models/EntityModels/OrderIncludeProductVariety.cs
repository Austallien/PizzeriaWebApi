using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.EntityModels
{
    [Table("OrderIncludeProductVariety")]
    public class OrderIncludeProductVariety
    {
        [Required]
        [Key]
        public int IdOrder { get; set; }

        [Required]
        [ForeignKey("IdOrder")]
        public Order Order { get; set; }

        [Required]
        [Key]
        public int IdProductVariety { get; set; }
        
        [Required]
        [ForeignKey("IdProductVariety")]
        public ProductVariety ProductVariety { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
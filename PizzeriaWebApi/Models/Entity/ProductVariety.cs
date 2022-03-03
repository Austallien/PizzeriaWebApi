using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{
    [Table("ProductVariety")]
    public class ProductVariety
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public int IdProduct { get; set; }

        [Required]
        [ForeignKey("IdProduct")]
        public Product Product{ get; set; }

        [Required]
        public int IdProductQuantity { get; set; }

        [Required]
        [ForeignKey("IdProductQuantity")]
        public ProductQuantity ProductQuantity { get; set; }

        [Required]
        [InverseProperty("ProductVariety")]
        public ICollection<OrderIncludeProductVariety> OrderIncludeProductVariety { get; set; }
    }
}
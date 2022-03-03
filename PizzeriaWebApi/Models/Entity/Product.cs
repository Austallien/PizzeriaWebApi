using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{
    [Table("Product")]
    public class Product
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        [InverseProperty("Product")]
        public ICollection<ProductVariety> ProductVarieties { get; set; }

        [Required]
        [InverseProperty("Product")]
        public ICollection<ProductIncludeIngridient> ProductIncludeIngridients { get; set; }
    }
}
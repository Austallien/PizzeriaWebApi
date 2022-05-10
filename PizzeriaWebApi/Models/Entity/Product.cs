using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models.Entity
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
        [StringLength(128)]
        public string ImagePath { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public int IdCategory { get; set; }

        [Required]
        [ForeignKey("IdCategory")]
        public Category Category { get; set; }

        [Required]
        [InverseProperty("Product")]
        public ICollection<ProductVariety> ProductVarieties { get; set; }

        [Required]
        [InverseProperty("Product")]
        public ICollection<ProductIncludeIngridient> ProductIncludeIngridients { get; set; }
    }
}
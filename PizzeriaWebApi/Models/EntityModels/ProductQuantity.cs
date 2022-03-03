using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.EntityModels
{
    [Table("ProductQuantity")]
    public class ProductQuantity
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Quantity { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public int IdQuantityMeasurementUnit { get; set; }

        [Required]
        [ForeignKey("IdQuantityMeasuremenetUnit")]
        public QuantityMeasurementUnit QuantityMeasurementUnit { get; set; }

        [Required]
        [InverseProperty("ProductQuantity")]
        public ICollection<ProductVariety> ProductVarieties { get; set; }
    }
}
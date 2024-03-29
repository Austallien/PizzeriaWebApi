using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models.Entity
{
    [Table("QuantityMeasurementUnit")]
    public class QuantityMeasurementUnit
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        [InverseProperty("QuantityMeasurementUnit")]
        public ICollection<ProductQuantity> ProductQuantities { get; set; }
    }
}
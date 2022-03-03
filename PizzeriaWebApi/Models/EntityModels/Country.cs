using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.EntityModels
{
    [Table("Country")]
    public class Country
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        [InverseProperty("Country")]
        public ICollection<DeliveryAddress> DeliveryAddresses { get; set; }

        [Required]
        [InverseProperty("Country")]
        public ICollection<Building> Buildings { get; set; }

        [Required]
        [InverseProperty("Country")]
        public ICollection<City> Cities { get; set; }
    }
}
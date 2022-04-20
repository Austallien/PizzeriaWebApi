using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models.Entity
{
    [Table("City")]
    public class City
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public int IdCountry { get; set; }

        [Required]
        [ForeignKey("IdCountry")]
        public Country Country { get; set; }

        [Required]
        [InverseProperty("City")]
        public ICollection<DeliveryAddress> DeliveryAddresses { get; set; }

        [Required]
        [InverseProperty("City")]
        public ICollection<Building> Buildings { get; set; }

        [Required]
        [InverseProperty("City")]
        public ICollection<CityHasStreet> CityHasStreets { get; set; }
    }
}
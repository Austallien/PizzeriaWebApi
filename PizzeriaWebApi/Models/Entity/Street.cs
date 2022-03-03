using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{
    [Table("Street")]
    public class Street
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        [InverseProperty("Street")]
        public ICollection<DeliveryAddress> DeliveryAddresses {get;set;}

        [Required]
        [InverseProperty("Street")]
        public ICollection<Building> Buildings { get; set; }

        [Required]
        [InverseProperty("Street")]
        public ICollection<CityHasStreet> CitiesHasStreet { get; set; }
    }
}
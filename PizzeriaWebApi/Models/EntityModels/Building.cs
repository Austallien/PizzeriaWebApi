using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.EntityModels
{
    [Table("Building")]
    public class Building
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public int IdCountry { get; set; }

        [Required]
        [ForeignKey("IdCountry")]
        public Country Country { get; set; }

        [Required]
        public int IdCity { get; set; }

        [Required]
        [ForeignKey("IdCity")]
        public City City{ get; set; }

        [Required]
        public int IdStreet { get; set; }

        [Required]
        [ForeignKey("IdStreet")]
        public Street Street { get; set; }

        [Required]
        [InverseProperty("Building")]
        public ICollection<Order> Orders { get; set; }

        [Required]
        [InverseProperty("Staff")]
        public ICollection<Staff> Staff { get; set; }
    }
}
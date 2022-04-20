using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models.Entity
{
    [Table("CityHasStreet")]
    public class CityHasStreet
    {
        [Required]
        public int IdCity { get; set; }

        [Required]
        [ForeignKey("IdCity")]
        public City City { get; set; }

        [Required]
        public int IdStreet { get; set; }

        [Required]
        [ForeignKey("IdStreet")]
        public Street Street { get; set; }
    }
}
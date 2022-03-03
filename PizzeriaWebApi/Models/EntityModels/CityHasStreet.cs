using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.EntityModels
{
    [Table("CityHasStreet")]
    public class CityHasStreet
    {
        [Required]
        [Key]
        public int IdCity { get; set; }

        [Required]
        [ForeignKey("IdCity")]
        public City City { get; set; }

        [Required]
        [Key]
        public int IdStreet { get; set; }

        [Required]
        [ForeignKey("IdStreet")]
        public Street Street { get; set; }
    }
}
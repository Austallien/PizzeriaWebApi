using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models.Entity
{
    [Table("SetHasProduct")]
    public class SetHasProduct
    {
        [Required]
        [Key]
        public int IdSet { get; set; }

        [Required]
        [ForeignKey("IdSet")]
        public Set Set { get; set; }

        [Required]
        [Key]
        public int IdProductVariety { get; set; }

        [Required]
        [ForeignKey("IdProductVariety")]
        public ProductVariety ProductVariety { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}

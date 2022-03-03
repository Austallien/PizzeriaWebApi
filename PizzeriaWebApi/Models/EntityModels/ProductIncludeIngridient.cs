using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.EntityModels
{
    [Table("ProductIncludeIngridient")]
    public class ProductIncludeIngridient
    {
        [Required]
        [Key]
        public int IdProduct { get; set; }

        [Required]
        [ForeignKey("IdProduct")]
        public Product Product { get; set; }

        [Required]
        [Key]
        public int IdIngridient { get; set; }

        [Required]
        [ForeignKey("IdIngridient")]
        public Ingridient Ingridient { get; set; }
    }
}
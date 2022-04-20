using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models.Entity
{
    [Table("Staff")]
    public class Staff
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public int IdUser { get; set; }

        [Required]
        [ForeignKey("IdUser")]
        public User User { get; set; }

        [Required]
        public int IdBuilding { get; set; }

        [Required]
        [ForeignKey("IdBuilding")]
        public Building Building { get; set; }
    }
}

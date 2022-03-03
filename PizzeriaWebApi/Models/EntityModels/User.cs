using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.EntityModels
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string MiddleName { get; set; }

        public string LastName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public int IdRole { get; set; }

        [Required]
        [ForeignKey("IdRole")]
        public Role Role { get; set; }

        [Required]
        [InverseProperty("User")]
        public ICollection<Client> Clients { get; set; }

        [Required]
        [InverseProperty("User")]
        public ICollection<Order> Orders { get; set; }

        [Required]
        [InverseProperty("Staff")]
        public ICollection<Staff> Staff { get; set; }
    }
}
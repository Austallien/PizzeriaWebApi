using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.EntityModels
{
    [Table("ReceivingMethod")]
    public class ReceivingMethod
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
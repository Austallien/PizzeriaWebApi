using System;

namespace Models.EntityModels
{
    public class Discount
    {
        public int Id { get; set; }
        public double Percent { get; set; }
        public double Threshold { get; set; }
        public bool IsDeleted { get; set; }
    }
}
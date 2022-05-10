using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Api.Models.Http
{
    public class Variety
    {
        public int Id { get; set; }
        public double Quantity { get; set; }
        public string MeasurementUnit { get; set; }
        public double Price { get; set; }
        public bool IsDeleted { get; set; }
    }
}
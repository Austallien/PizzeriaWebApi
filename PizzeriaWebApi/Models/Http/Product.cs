using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Api.Models.Http
{
    public class Product
    {
        public int Id { get; set; }
        public int VarietyId { get; set; }
        public PhysicalFileResult Image { get; set; }
        public string Name { get; set; }
        public string QuantityName { get; set; }
        public decimal QuantityValue { get; set; }
        public string MeasurementQuantityUnit { get; set; }
        public decimal Price { get; set; }
    }
}
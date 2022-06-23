namespace Api.Models.Http.Send
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
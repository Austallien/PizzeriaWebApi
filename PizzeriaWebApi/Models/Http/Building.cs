namespace Api.Models.Http
{
    public class Building
    {
        public int Id { get; set; }
        public int IdCountry { get; set; }
        public int IdCity { get; set; }
        public int IdStreet { get; set; }
        public string Number { get; set; }
    }
}
namespace Api.Models.Http.Send
{
    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Role{ get; set; }
        public string Login { get; set; }
        public decimal TotalOrderPrice { get; set; }
        public decimal Discount { get; set; }

        public static Client ConvertFromEntityUser(Models.Entity.User User)
        {
            return new Client
            {
                Id = User.Id,
                FirstName = User.FirstName,
                MiddleName = User.MiddleName,
                LastName = User.LastName,
                Role = User.IdRole+"",
                Login = User.Login
            };
        }
    }
}
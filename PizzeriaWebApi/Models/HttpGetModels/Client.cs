using System;

namespace Models.HttpGetModels
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

        public static Client ConvertFromEntityUser(Models.EntityModels.User User)
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
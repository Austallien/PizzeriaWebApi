namespace Api.Models.Http.Send
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Role { get; set; }

        public static User GetInstance(Models.Entity.User User)
        {
            return new User
            {
                Id = User.Id,
                FirstName = User.FirstName,
                MiddleName = User.MiddleName,
                LastName = User.LastName,
                Role = User.Role is null ? User.IdRole + "" : User.Role.Name,
                Login = User.Login
            };
        }
    }
}
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Api.Models
{
    public class AuthOptions
    {
        public const string ISSUER = "Server"; //supplier
        public const string AUDIENCE = "Client"; //consumer
        const string KEY = "x8QUIKpP0RBXS4fXWszPs7Tf"; //ecnryptuion key
        public const int LIFETIME_IN_MINUTES = 1; //minutes

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
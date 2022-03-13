using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Models
{
    public class AuthOptions
    {
        public const string ISSUER = "Server"; //supplier
        public const string AUDIENCE = "Client"; //consumer
        const string KEY = "ecryption_super_secret_key"; //ecnryptuion key
        public const int LIFETIME = 20; //minutes

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            string key = GenerateNewKey(); //encryption key
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }

        private static string GenerateNewKey()
        {
            string key = "";
            for(int i = 0; i < 32; i++)
                key += (char)new Random(new Random().Next(1024)).Next(0,256);

            return key;
        }
    }
}
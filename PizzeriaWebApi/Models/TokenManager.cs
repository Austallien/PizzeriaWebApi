using Microsoft.IdentityModel.Tokens;
using System.Text.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Models
{
    public class TokenManager
    {
        readonly static string JWTPattern = "{{\"accessToken\":\"{0}\"," + "\"refreshToken\":\"{1}\"}}";
        readonly static string accessJWTPattern = "{{\"accessToken\":\"{0}\"}}";

        /// <summary>
        /// Generates pair of access and refresh JWTs
        /// </summary>
        /// <param name="Identity">User identity</param>
        /// <returns>JSON string which contains JWTs - accessToken & refreshToken</returns>
        public static string GenerateJWTs(ClaimsIdentity Identity)
        {
            var accessToken = GenerateAccessToken(Identity);
            var refreshToken = GenerateRefreshToken(Identity);
            return String.Format(JWTPattern, accessToken, refreshToken).ToString();
        }

        /// <summary>
        /// Generates new access JWT if the old one is outdated
        /// </summary>
        /// <param name="Identity">User identity</param>
        /// <returns>JSON string which contains JWT - accessToken</returns>
        public static string GenerateAccessJWT(ClaimsIdentity Identity)
        {
            string accessToken = GenerateAccessToken(Identity);
            return String.Format(accessJWTPattern, accessToken).ToString();
        }

        private static string GenerateAccessToken(ClaimsIdentity Identity)
        {
            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: Identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME_IN_MINUTES)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        private static string GenerateRefreshToken(ClaimsIdentity Identity)
        {
            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: Identity.Claims,
                expires: now.Add(TimeSpan.FromDays(2)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}
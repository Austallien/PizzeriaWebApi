using Microsoft.IdentityModel.Tokens;
using System.Text.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Models
{
    public class TokenManager
    {
        public struct TokenGroup
        {
            public string AccessJwtToken;
            public string RefreshJwtToken;
        }

        public static TokenGroup GenerateTokenGroup(ClaimsIdentity Identity)
        {
            return new TokenGroup
            {
                AccessJwtToken = GenerateAccessToken(Identity),
                RefreshJwtToken = GenerateRefreshToken(Identity)
            };
        }

        public static bool ValidateToken(TokenGroup TokenGroup)
        {
            /*bool isAccessTokenValidated = true;
            bool isRefreshTokenValidated = true;

            JwtSecurityToken accessToken = new JwtSecurityTokenHandler().ReadJwtToken(TokenGroup.AccessJwtToken);
            JwtSecurityToken refreshToken = new JwtSecurityTokenHandler().ReadJwtToken(TokenGroup.RefreshJwtToken);

            if (accessToken.ValidTo.Ticks > DateTime.UtcNow.Ticks)
                isAccessTokenValidated = false;*/

            return false;

        }

        private static string GenerateAccessToken(ClaimsIdentity Identity)
        {
            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: Identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
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
                expires: now.Add(TimeSpan.FromDays(365)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}
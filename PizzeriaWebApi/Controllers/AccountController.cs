using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;

namespace Api.Controllers
{
    [Route("account")]
    [Authorize]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly Models.Entity.Context _context;

        public AccountController(Models.Entity.Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Authenticate user which try to sign in
        /// </summary>
        /// <param name="Login">User login</param>
        /// <param name="Password">User password</param>
        /// <returns>Access and refresh JWTs</returns>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        async public Task<ActionResult<string>> Authenticate(string Login, string Password)
        {
            string result = await Task.Run(() =>
            {
                var identity = GetIdentity(Login, Password);
                if (identity == null)
                    return "Invalid username or password";

                var tokens = TokenManager.GenerateJWTs(identity);

                return tokens;
            });

            return result;
        }

        /// <summary>
        /// Authorizes user by login which included in JWT
        /// </summary>
        /// <returns>Authenticated user data</returns>
        [Authorize]
        [HttpPost("authorize")]
        async public Task<ActionResult<Models.Http.Send.User>> Authorize()
        {
            Models.Http.Send.User user = await Task.Run(() =>
            {
                string login = GetLoginFromJWT(Request);
                Models.Http.Send.User user = (from item in _context.User where item.Login.Equals(login) select Models.Http.Send.User.GetInstance(item)).FirstOrDefault();
                return user;
            });
            if (user == null)
                return BadRequest("UserNotFound");
            return user;
        }

        /// <summary>
        /// Refreshes access JWT if the refresh token is valid
        /// </summary>
        /// <param name="Login">User login</param>
        /// <param name="Password">User password</param>
        /// <returns>New access JWT</returns>
        [Authorize]
        [HttpPost("refreshAccessToken")]
        async public Task<ActionResult<string>> RefreshAccessToken()
        {
            string result = await Task.Run(() =>
            {
                var identity = GetIdentityByRefreshJWT();

                string accessToken = TokenManager.GenerateAccessJWT(identity);

                return accessToken;
            });

            return result;
        }

        [AllowAnonymous]
        [HttpPost("register:firstName={FirstName}&middleName={MiddleName}&lastName={LastName}&phoneNumber={PhoneNumber}&login={Login}&password={Password}")]
        async public Task<ActionResult<bool>> Register(string FirstName, string MiddleName, string LastName, string PhoneNumber, string Login, string Password)
        {
            bool _result = await Task<bool>.Run(() =>
            {
                try
                {
                    var user = _context.User.Add(new Models.Entity.User()
                    {
                        FirstName = FirstName,
                        MiddleName = MiddleName,
                        LastName = LastName,
                        PhoneNumber = PhoneNumber,
                        IdRole = 2004, //Client
                        Login = Login,
                        Password = PasswordCryptograph.Encrypt(Password),
                        IsDeleted = false
                    });

                    var client = _context.Client.Add(new Models.Entity.Client()
                    {
                        IdUser = user.Entity.Id,
                        IdDiscount = 1,
                        IsDeleted = false
                    });

                    _context.SaveChanges();

                    return true;
                }
                catch (System.Exception ex)
                {
                    return false;
                }
            });

            return _result;
        }

        private ClaimsIdentity GetIdentity(string Login, string Password)
        {
            if (Login == null || Password == null)
                return null;

            string encryptedPassword = PasswordCryptograph.Encrypt(Password);

            var user = (from item in _context.User 
                        where item.Login.Equals(Login) && item.Password.Equals(encryptedPassword)
                        select new {
                                    Login = item.Login,
                                    Role = item.Role.Name
                                }).FirstOrDefault();
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
                };

                ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }

        private ClaimsIdentity GetIdentityByRefreshJWT()
        {
            string login = GetLoginFromJWT(Request);
            string acasddacessToken = Request.Headers[HeaderNames.Authorization].ToString();
            var user = (from item in _context.User where item.Login.Equals(login) select
                            new
                            {
                                Login = item.Login,
                                Role = item.Role.Name
                            }).FirstOrDefault();
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
                };

                ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }

        public static string GetLoginFromJWT(Microsoft.AspNetCore.Http.HttpRequest Request)
        {
            string rawToken = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var actualToken = new JwtSecurityTokenHandler().ReadJwtToken(rawToken);
            string login = actualToken.Claims.First(item => item.Type.Equals(ClaimsIdentity.DefaultNameClaimType)).Value;
            return login;
        }
    }
}
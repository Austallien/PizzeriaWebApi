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

        [AllowAnonymous]
        [HttpGet("asd:login={login}&password={password}")]
        public IActionResult asd(string login, string password)
        {
            Models.Entity.User user = _context.User.FirstOrDefault(item => item.Login.Equals(login));
          /* foreach(var item in _context.User)
            {
                item.Password = PasswordCryptograph.Encrypt(item.Password);
            }
            _context.SaveChanges();*/
            string asd = PasswordCryptograph.Encrypt(user.Password);
            return Content(password + "\n" 
                + asd + "\n" 
                + user.Password + "\n"
                + PasswordCryptograph.Validate(password, user.Password));

            //return PasswordCryptograph.Validate(password, user.Password);
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
        async public Task<ActionResult<Models.Http.User>> Authorize()
        {
            Models.Http.User user = await Task.Run(() =>
            {
                string login = GetLoginFromJWT(Request);
                Models.Http.User user = (from item in _context.User where item.Login.Equals(login) select Models.Http.User.GetInstance(item)).FirstOrDefault();
                return user;
            });
            if (user == null)
                return BadRequest("UserNotFound");
            return user;
        }

        [Authorize]
        [HttpPost("data")]
        async public Task<IActionResult> Data()
        {
            return Content("NotImplementedMethod");
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
        [HttpPost("register")]
        async public Task<ActionResult<bool>> Register(string FirstName, string MiddleName, string LastName, string PhoneNumber, string Login, string Password)
        {
            await _context.User.AddAsync(new Models.Entity.User()
            {
                FirstName = FirstName,
                MiddleName = MiddleName,
                LastName = LastName,
                PhoneNumber = PhoneNumber,
                Login = Login,
                Password = PasswordCryptograph.Encrypt(Password)
            });
            return true;
        }

        private ClaimsIdentity GetIdentity(string Login, string Password)
        {
            string encryptedPassword = PasswordCryptograph.Encrypt(Password);

            var user = (from item in _context.User where item.Login.Equals(Login) && item.Password.Equals(encryptedPassword) select
                                new {
                                    Login = item.Login,
                                    Role = item.Role.Name
                                }).FirstOrDefault();
            if(user != null)
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
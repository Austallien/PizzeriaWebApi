using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Entity;
using Models;

namespace WebApplication1.Controllers
{
    [Route("account")]
    [Authorize]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly Context _context;

        public AccountController(Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Authenticate user which try to sign in
        /// </summary>
        /// <param name="Login">User login</param>
        /// <param name="Password">User password</param>
        /// <returns>New pair of access and refresh JWT with user id in JSON scheme</returns>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        async public Task<ActionResult<string>> Authenticate(string Login, string Password)
        {
            try
            {
                string result = await Task.Run(() =>
                {
                    var identity = GetIdentity(Login, Password);
                    if (identity == null)
                        return "Invalid username or password";

                    var user = _context.User.FirstOrDefault(item => item.Login.Equals(Login) && item.Password.Equals(Password));

                    var tokens = TokenManager.GenerateTokenGroup(identity);
                    var accessJwtToken = tokens.AccessJwtToken;
                    var refreshJwtToken = tokens.RefreshJwtToken;
                    var result = "{\"accessJwtToken\":\"" + accessJwtToken + "\",\"refreshJwtToken\":\"" + refreshJwtToken + "\",\"userId\":\"" + user.Id + "\"}";

                    return result;
                });

                if (result.Length > "{\"accessJwtToken\":\"\",\"refreshJwtToken\":\"\",\"userId\":\"\"}".Length)
                    return result;
                else throw new System.Exception();
            }
            catch
            {
                return BadRequest("InternalServerError");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost("authorize")]
        async public Task<ActionResult<Models.HttpGetModels.User>> Authorize(int UserId)
        {
            try
            {
                Models.HttpGetModels.User user = await Task.Run(() =>
                {
                    Models.HttpGetModels.User user = (from item in _context.User where item.Id == UserId select Models.HttpGetModels.User.GetInstance(item)).FirstOrDefault();
                    return user;
                });
                if(user == null)
                    return BadRequest("UserNotFound");
                return user;
            }
            catch
            {
                return BadRequest("InternalServerError");
            }
        }

        /// <summary>
        /// Refreshes access JWT if the refresh token is valid
        /// </summary>
        /// <param name="Login">User login</param>
        /// <param name="Password">User password</param>
        /// <returns>New access JWT in JSON scheme</returns>
        [Authorize]
        [HttpPost("refreshAccessToken")]
        public IActionResult RefreshAccessToken(string Login, string Password)
        {
            return BadRequest("NotImplementedMethod");
        }

        [Authorize]
        [HttpPost("Test")]
        public string GetTestData()
        {
            return "TestDataReceived";
        }

        [Authorize]
        [HttpPost]
        public IActionResult Asd(TokenManager.TokenGroup TokenGroup)
        {
            var result = TokenManager.ValidateToken(TokenGroup);
            //if(result)
                return Content("NotImplementedMethod; result = " + result.ToString());
        }


        private ClaimsIdentity GetIdentity(string Login, string Password)
        {
            var user = (from item in _context.User where item.Login.Equals(Login) && item.Password.Equals(Password) select
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
    }
}
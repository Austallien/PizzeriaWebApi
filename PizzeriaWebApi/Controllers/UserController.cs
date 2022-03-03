using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Entity;

namespace WebApplication1.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly PizzeriaContext _context;

        public UserController(PizzeriaContext context)
        {
            _context = context;
        }

        [HttpGet("auth:login={Login}&password={Password}")]
        public async Task<ActionResult<Models.HttpGetModels.Client>> GetOrderList(string Login, string Password)
        {

           /* var store = new X509Store(StoreName.Root, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly); 
            var certificate = store.Certificates.OfType<X509Certificate2>()
                .First(c => c.FriendlyName == "PizzeriaApiCertificate");*/

            var user =  _context.User.FirstOrDefault(user=>user.Login.Equals(Login) && user.Password.Equals(Password) && user.IdRole == 2004);

            if (user == null)
                return null;
            return Models.HttpGetModels.Client.ConvertFromEntityUser(user);
        }
    }
}
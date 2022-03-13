using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Entity;

namespace WebApplication1.Controllers
{
    [Route("data")]
    [ApiController]
    [Authorize]
    public class DataController : ControllerBase
    {
        private readonly Context _context;

        public DataController(Context context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost("get:orderListByUserId={IdUser}")]
        public async Task<ActionResult<IEnumerable<Models.HttpGetModels.Order>>> GetOrderList(long IdUser)
        {
            return null;//Models.HttpGetModels.Order.GetFromDatabaseByUserId(_context, IdUser);
        }

        //[AllowAnonymous]
        [Authorize]
        [HttpGet("get:orderData={IdOrder}")]
        public async Task<Models.HttpGetModels.Order> GetOrderItem(int IdOrder)
        {
            Models.HttpGetModels.Order order = (from item in _context.Order where item.Id == IdOrder select new Models.HttpGetModels.Order()
            {
                Id = item.Id,
                RegistrationDate = (long)item.RegistrationDate.Date.Subtract(new DateTime(1970, 1, 1)).TotalSeconds * 1000,
                RegistrationTime = item.RegistrationTime.Ticks,
                ReceivingDate = item.ReceivingDate.Date.Ticks,
                ReceivingTime = item.ReceivingTime.Ticks,
                ReceivingMethod = item.ReceivingMethod.Name,
                Address = item.IdBuilding.ToString(),
                DeliveryAddress = item.IdBuilding.ToString(),
                TotalPrice = Convert.ToDouble(item.TotalPrice),
                Status = item.OrderStatus.Name
            }).FirstOrDefault();

            return order;
        }
    }
}
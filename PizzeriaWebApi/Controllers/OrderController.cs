using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Entity;

namespace WebApplication1.Controllers
{
    [Route("order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly PizzeriaContext _context;

        public OrderController(PizzeriaContext context)
        {
            _context = context;
        }

        [HttpGet("get:idUser={IdUser}")]
        public async Task<ActionResult<IEnumerable<Models.HttpGetModels.Order>>> GetOrderList(long IdUser)
        {
            return null;//Models.HttpGetModels.Order.GetFromDatabaseByUserId(_context, IdUser);
        }

        [HttpGet("get:idOrder={IdOrder}")]
        public async Task<Models.HttpGetModels.Order> GetOrderItem(int IdOrder)
        {
            //var order = Models.HttpGetModels.Order.GetFromDatabaseByOrderId(_context, IdOrder);
            //return await _context.Order.FirstOrDefaultAsync(item => item.Id == IdOrder);
            /*var list = (from item in _context.Order
                        select new Models.HttpGetModels.Order()
                        {
                            Id = item.Id,
                            RegistrationDate = 1,
                            RegistrationTime = 1,
                            ReceivingDate = 1,
                            ReceivingTime = 1,
                            ReceivingMethod = "1",
                            Address = "1",
                            DeliveryAddress = "1",
                            TotalPrice = 1,
                            Status = item.OrderStatus.Name
                        }).ToList<Models.HttpGetModels.Order>();

            var list1 = (from item in _context.Order
                        select item);*/

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

            /*var dbOrder = await _context.Order.FindAsync(IdOrder);
            var order = Models.HttpGetModels.Order.ConvertFromDbOrder(dbOrder);*/
            return order;
              /*  if (order == null)
                    return NotFound();
            return order;   */
        }
    }
}
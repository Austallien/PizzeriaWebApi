using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.EntityModels;

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
            return Models.HttpGetModels.Order.GetFromDatabaseByUserId(_context, IdUser);
        }

        [HttpGet("get:idOrder={IdOrder}")]
        public async Task<ActionResult<Models.HttpGetModels.Order>> GetOrderItem(int IdOrder)
        {
            var order = Models.HttpGetModels.Order.GetFromDatabaseByOrderId(_context, IdOrder);

            /*var dbOrder = await _context.Order.FindAsync(IdOrder);
            var order = Models.HttpGetModels.Order.ConvertFromDbOrder(dbOrder);*/

                if (order == null)
                    return NotFound();
            return order;   
        }
    }
}
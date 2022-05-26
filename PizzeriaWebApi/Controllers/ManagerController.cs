using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Models;
using System.IO;
using System.Reflection;
using Api.Models.Http.Building.Send;

namespace Api.Controllers
{
    [Route("manager")]
    [ApiController]
    [Authorize]
    public class ManagerController : ControllerBase
    {
        private readonly Models.Entity.Context _context;

        public ManagerController(Models.Entity.Context context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost("data?building={IdBuilding}")]
        public async Task<ActionResult<object>> GetGeneralData(int IdBuilding)
        {
            var result = new
            {
                Status = new List<Status>(),
                Receiving = new List<Receiving>(),
                Order = new List<Order>()
            };

            var Status = await (from item in _context.OrderStatus
                                select new Status()
                                {
                                    Id = item.Id,
                                    Name = item.Name
                                }).ToListAsync();

            var Receiving = await (from item in _context.ReceivingMethod
                                   select new Receiving()
                                   {
                                       Id = item.Id,
                                       Name = item.Name
                                   }).ToListAsync();

            var Order = await (from item in _context.Order
                               where item.IdBuilding == IdBuilding
                               select new Order()
                               {
                                   Id = item.Id,
                                   RegistrationDate = item.RegistrationDate.Ticks,
                                   RegistrationTime = item.RegistrationTime.Ticks,
                                   ReceivingDate = item.ReceivingDate.Ticks,
                                   ReceivingTime = item.ReceivingTime.Ticks,
                                   Total = (double)item.TotalPrice,
                                   IdStatus = item.IdStatus,
                                   IdReceiving = item.IdReceivingMethod
                               }).ToListAsync();

            result.Status.AddRange(Status);
            result.Receiving.AddRange(Receiving);
            result.Order.AddRange(Order);


            return result;
        }
    }
}
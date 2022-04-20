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

namespace Api.Controllers
{
    [Route("data")]
    [ApiController]
    [Authorize]
    public class DataController : ControllerBase
    {
        private readonly Models.Entity.Context _context;

        public DataController(Models.Entity.Context context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost("general")]
        public async Task<ActionResult<IEnumerable<Models.Http.Product>>>GetGeneralData()
        {
            List<Models.Http.Product> products = new List<Models.Http.Product>();

            List<Models.Http.Product> list = await (from item in _context.ProductVariety
            select new Models.Http.Product()
            {
                Id = item.Product.Id,
                VarietyId = item.Id,
                Name = item.Product.Name,
                QuantityName = item.ProductQuantity.Name,
                QuantityValue = item.ProductQuantity.Quantity,
                MeasurementQuantityUnit = item.ProductQuantity.QuantityMeasurementUnit.Name,
                Price = item.Price
            }).ToListAsync();

            await Task.Run(() =>
            {
                foreach (var item in _context.ProductVariety)
                {
                    string path = Path.Combine(Environment.CurrentDirectory, item.Product.ImagePath);
                    string type = "application/webp";
                    PhysicalFileResult result = PhysicalFile(path, type, item.Product.ImagePath.Split('/').Last());

                    list.FirstOrDefault(_item => _item.Id == item.Product.Id).Image = result;
                }
            });

            return list;
        }

        [Authorize]
        [HttpPost("get:orderListByUserId={IdUser}")]
        public async Task<ActionResult<IEnumerable<Models.Http.Order>>> GetOrderList(long IdUser)
        {
            return null;//Models.HttpGetModels.Order.GetFromDatabaseByUserId(_context, IdUser);
        }

        //[AllowAnonymous]
        [Authorize]
        [HttpGet("get:orderData={IdOrder}")]
        public async Task<Models.Http.Order> GetOrderItem(int IdOrder)
        {
            Models.Http.Order order = (from item in _context.Order where item.Id == IdOrder select new Models.Http.Order()
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
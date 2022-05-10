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


        [AllowAnonymous]
        [HttpPost("food/general")]
        public async Task<ActionResult<IEnumerable<Models.Http.Product>>> GetGeneralData()
        {
            List<Models.Http.Product> products = await (from item in _context.Product
                                                        select new Models.Http.Product()
                                                        {
                                                            Id = item.Id,
                                                            Name = item.Name,
                                                            Category = item.Category.Name,
                                                            Image = item.ImagePath,
                                                            Varieties = (from variety in _context.ProductVariety
                                                                         where variety.Product.Id == item.Id
                                                                         select new Models.Http.Variety()
                                                                         {
                                                                             Id = variety.Id,
                                                                             Quantity = (double)variety.ProductQuantity.Quantity,
                                                                             MeasurementUnit = variety.ProductQuantity.QuantityMeasurementUnit.Name,
                                                                             Price = (double)variety.Price,
                                                                             IsDeleted = variety.IsDeleted
                                                                         }).ToList(),
                                                            Composition = (from composition in item.ProductIncludeIngridients
                                                                           select composition.Ingridient.Name).ToList(),
                                                            IsDeleted = item.IsDeleted
                                                        }).ToListAsync();

            return products;
        }

        [AllowAnonymous]
        [HttpGet("image&name={Name}"),HttpPost("image&name={Name}")]
        public async Task<ActionResult> Image(string Name)
        {
            string path = Environment.CurrentDirectory + "\\content\\image\\";
            string image = Name;
            string format = Name.Split('.')[1];
            return PhysicalFile(path + Name, "image/" + format);
        }

       /* [AllowAnonymous]
        [HttpPost("food/categories")]
        public async Task<ActionResult<IEnumerable<Models.Http.Category>>> GetCategoriesData()
        {
            List<Models.Http.Category> categories = await (from item in _context.Category select new Models.Http.Category()
            {
                Id = item.Id,
                Name = item.Name,
                Products = (from item in )
            }).ToListAsync();
        }*/

        [AllowAnonymous]
        [HttpPost("food/sets")]
        public async Task<ActionResult<IEnumerable<Models.Http.Set>>> GetSetData()
        {
            List<Models.Http.Set> sets = await (from item in _context.Set
                                                select new Models.Http.Set()
                                                {
                                                    Id = item.Id,
                                                    Name = item.Name,
                                                    IsDeleted = item.IsDeleted,
                                                    Products = (from set in _context.SetHasProduct
                                                                where set.IdSet == item.Id
                                                                select set.IdProductVariety).ToList()
                                                }).ToListAsync();
            return sets;
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
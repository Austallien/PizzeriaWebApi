using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<IEnumerable<Models.Http.Send.Product>>> GetGeneralData()
        {
            List<Models.Http.Send.Product> products = await (from item in _context.Product where item.ProductVarieties.Count > 0
                                                        select new Models.Http.Send.Product()
                                                        {
                                                            Id = item.Id,
                                                            Name = item.Name,
                                                            Category = item.Category.Name,
                                                            Image = item.ImagePath,
                                                            Varieties = (from variety in _context.ProductVariety
                                                                         where variety.Product.Id == item.Id
                                                                         select new Models.Http.Send.Variety()
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
        [HttpPost("address")]
        public async Task<ActionResult<Models.Http.Send.Geolocation>> GetAddresses()
        {
            Models.Http.Send.Geolocation geolocation = new Models.Http.Send.Geolocation()
            {
                Countries = await (from country in _context.Country
                                   select new Models.Http.Send.Geolocation.Country()
                                   {
                                       Id = country.Id,
                                       Name = country.Name,
                                       Cities = (from city in country.Cities
                                                 select new Models.Http.Send.Geolocation.City()
                                                 {
                                                     Id = city.Id,
                                                     Name = city.Name,
                                                     Streets = (from street in city.CityHasStreets
                                                                select new Models.Http.Send.Geolocation.Street()
                                                                {
                                                                    Id = street.Street.Id,
                                                                    Name = street.Street.Name
                                                                }).ToList()
                                                 }).ToList()
                                   }).ToListAsync()
            };


            return geolocation;
        }

        [AllowAnonymous]
        [HttpPost("location/building")]
        public async Task<ActionResult<List<Models.Http.Send.Building>>> GetBuildings()
        {
            List<Models.Http.Send.Building> list = await (from item in _context.Building
                                                     select new Models.Http.Send.Building()
                                                     {
                                                         Id = item.Id,
                                                         IdCountry = item.IdCountry,
                                                         IdCity = item.IdCity,
                                                         IdStreet = item.IdStreet,
                                                         Number = item.Number
                                                     }).ToListAsync();

            return list;
        }

        [Authorize]
        [HttpPost("order/load")]
        public async Task<ActionResult<List<Object>>> OrderList()
        {
            string login = AccountController.GetLoginFromJWT(Request);

            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            List<Models.Http.Send.Receiving> receiving = await (from item in _context.ReceivingMethod
                                                                select new Models.Http.Send.Receiving()
                                                                {
                                                                    Id = item.Id,
                                                                    Name = item.Name
                                                                }).ToListAsync();

            List<Models.Http.Send.Discount> discount = await (from item in _context.Discount
                                                              select new Models.Http.Send.Discount()
                                                              {
                                                                  Id = item.Id,
                                                                  Value = (double)item.Value
                                                              }).ToListAsync();

            List<Models.Http.Send.Status> status = await (from item in _context.OrderStatus
                                                          select new Models.Http.Send.Status()
                                                          {
                                                              Id = item.Id,
                                                              Name = item.Name
                                                          }).ToListAsync();

            List<Models.Http.Send.Order> order = await (from item in _context.Order
                                                       where item.User.Login.Equals(login)
                                                       select new Models.Http.Send.Order()
                                                       {
                                                           Id = item.Id,
                                                           RegistrationDate = item.RegistrationDate.Ticks - sTime.Ticks,
                                                           RegistrationTime = item.RegistrationTime.Ticks,
                                                           ReceivingDate = item.ReceivingDate.Ticks - sTime.Ticks,
                                                           ReceivingTime = item.ReceivingTime.Ticks,
                                                           Content = (from content in item.OrderIncludeProductVariety 
                                                                      select new Models.Http.Send.OrderContentItem()
                                                                      {
                                                                          IdProduct = content.ProductVariety.IdProduct,
                                                                          IdVariety = content.ProductVariety.Id,
                                                                          Amount = content.Amount
                                                                      }).ToList(),
                                                           Delivery = item.Delivery != null? item.Delivery.First().DeliveryAddress.City.Name + " " +
                                                                                                    item.Delivery.First().DeliveryAddress.Street.Name + " " +
                                                                                                    item.Delivery.First().DeliveryAddress.Number :
                                                                                                    "",
                                                           IdReceivingMethod = item.IdReceivingMethod,
                                                           IdBuilding = item.IdBuilding,
                                                           TotalPrice = (double)item.TotalPrice,
                                                           IdDiscount = _context.Discount.First().Id,
                                                           IdStatus = item.IdStatus
                                                       }).ToListAsync();

            List<object> data = new List<object>();
            data.Add(receiving);
            data.Add(discount);
            data.Add(status);
            data.Add(order);
            return data;
        }

        [Authorize]
        [HttpPost("order/place:building={BuildingId}&data={Data}")]
        public async Task<bool> PlaceOrder(int BuildingId, string Data)
        {
            bool result = await Task<bool>.Run(() =>
            {
                try
                {
                    var data = new
                    {
                        A = new List<int>(),
                        B = new List<int>()
                    };

                    decimal totalPrice = 0;

                    foreach (string item in Data.Split('_'))
                    {
                        int varietyId = Convert.ToInt32(item.Split('+')[0]);
                        int amount = Convert.ToInt32(item.Split('+')[1]);
                        data.A.Add(varietyId);
                        data.B.Add(amount);

                        totalPrice += _context.ProductVariety.FirstOrDefault(item => item.Id == varietyId).Price * amount;
                    }

                    string login = AccountController.GetLoginFromJWT(Request);
                    int id = _context.User.FirstOrDefault(item => item.Login.Equals(login)).Id;
                    double discount = (double)(from item in _context.Client where item.IdUser == id select item.Discount.Value).First();

                    Models.Entity.Order order = new Models.Entity.Order()
                    {
                        IdUser = id,
                        RegistrationDate = DateTime.Now,
                        RegistrationTime = DateTime.Now.TimeOfDay,
                        ReceivingDate = DateTime.MinValue,
                        ReceivingTime = DateTime.MinValue.TimeOfDay,
                        IdBuilding = BuildingId,
                        IdReceivingMethod = 1,
                        TotalPrice = totalPrice,
                        IdStatus = 1,
                        IsDeleted = false
                    };

                    _context.Order.Add(order);

                    order.OrderIncludeProductVariety = new List<Models.Entity.OrderIncludeProductVariety>();

                    int iterator = 0;
                    foreach (string item in Data.Split('_'))
                    {
                        order.OrderIncludeProductVariety.Add(new Models.Entity.OrderIncludeProductVariety()
                        {
                            IdOrder = order.Id,
                            IdProductVariety = data.A.ElementAt(iterator),
                            Amount = data.B.ElementAt(iterator++),
                            IsDeleted = false
                        });
                    }

                    _context.SaveChangesAsync();

                    return true;
                }
                catch(Exception ex)
                {
                    return false;
                }
            });

            return result;
        }

        [AllowAnonymous]
        [HttpGet("image:name={Name}"),HttpPost("image:name={Name}")]
        public async Task<ActionResult> Image(string Name)
        {
            string path = Environment.CurrentDirectory + "\\content\\image\\";
            string image = Name;
            string format = Name.Split('.')[1];
            return PhysicalFile(path + Name, "image/" + format);
        }

       /* [AllowAnonymous]
        [HttpPost("food/categories")]
        public async Task<ActionResult<IEnumerable<Models.Http.Send.Category>>> GetCategoriesData()
        {
            List<Models.Http.Send.Category> categories = await (from item in _context.Category select new Models.Http.Send.Category()
            {
                Id = item.Id,
                Name = item.Name,
                Products = (from item in )
            }).ToListAsync();
        }*/

        [AllowAnonymous]
        [HttpPost("food/sets")]
        public async Task<ActionResult<IEnumerable<Models.Http.Send.Set>>> GetSetData()
        {
            List<Models.Http.Send.Set> sets = await (from item in _context.Set
                                                select new Models.Http.Send.Set()
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

        ////[AllowAnonymous]
        //[Authorize]
        //[HttpGet("get:orderData={IdOrder}")]
        //public async Task<Models.Http.Send.Order> GetOrderItem(int IdOrder)
        //{
        //    Models.Http.Send.Order order = (from item in _context.Order where item.Id == IdOrder select new Models.Http.Send.Order()
        //    {
        //        Id = item.Id,
        //        RegistrationDate = (long)item.RegistrationDate.Date.Subtract(new DateTime(1970, 1, 1)).TotalSeconds * 1000,
        //        RegistrationTime = item.RegistrationTime.Ticks,
        //        ReceivingDate = item.ReceivingDate.Date.Ticks,
        //        ReceivingTime = item.ReceivingTime.Ticks,
        //        ReceivingMethod = item.ReceivingMethod.Name,
        //        Address = item.IdBuilding.ToString(),
        //        DeliveryAddress = item.IdBuilding.ToString(),
        //        TotalPrice = Convert.ToDouble(item.TotalPrice),
        //        Status = item.OrderStatus.Name
        //    }).FirstOrDefault();

        //    return order;
        //}
    }
}
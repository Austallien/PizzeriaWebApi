using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Api.Models.Http.Building.Send;

namespace Api.Controllers
{
    [Route("manager")]
    [ApiController]
    [Authorize]
    public class ManagerController : ControllerBase
    {
        private readonly PizzeriaDatabaze.Models.Entity.EntityContext _context;

        public ManagerController()
        {
            _context = new PizzeriaDatabaze.Models.Entity.EntityContext();
        }

        [Authorize]
        [HttpPost("data:building={IdBuilding}")]
        public async Task<ActionResult<object>> GetGeneralData(int IdBuilding)
        {
            object result = new object();

            await Task.Run(() =>
            {

                var Status = (from item in _context.OrderStatus
                              select new Status()
                              {
                                  Id = item.Id,
                                  Name = item.Name
                              }).ToList();

                var Receiving = (from item in _context.ReceivingMethod
                                 select new Receiving()
                                 {
                                     Id = item.Id,
                                     Name = item.Name
                                 }).ToList();

                /*  var Order = (from item in _context.Order
                               where item.IdBuilding == IdBuilding
                               select new Order()
                               {
                                   Id = item.Id,
                                   RegistrationDate = item.RegistrationDate.Millisecond,
                                   RegistrationTime = item.RegistrationTime.Ticks,
                                   ReceivingDate = item.ReceivingDate.Millisecond,
                                   ReceivingTime = item.ReceivingTime.Ticks,
                                   Total = (double)item.TotalPrice,
                                   IdStatus = item.IdStatus,
                                   IdReceiving = item.IdReceivingMethod
                               }).ToList();*/

                var Order = _context.Order.
                Where(item => item.IdBuilding == IdBuilding).
                Select(item => new Order()
                {
                    Id = item.Id,
                    RegistrationDate = item.RegistrationDate,
                    RegistrationTime = item.RegistrationTime.ToString(),
                    ReceivingDate = item.ReceivingDate,
                    ReceivingTime = item.ReceivingTime.ToString(),
                    Total = (double)item.TotalPrice,
                    IdStatus = item.IdStatus,
                    IdReceiving = item.IdReceivingMethod,
                    Products = (from variety in item.OrderIncludeProductVariety
                                select new Models.Http.Send.Product()
                                {
                                    Id = variety.ProductVariety.Product.Id,
                                    Name = variety.ProductVariety.Product.Name,
                                    Category = variety.ProductVariety.Product.Category.Name,
                                    Composition = (from composition in variety.ProductVariety.Product.Ingridient
                                                   select composition.Name).ToList(),
                                    Varieties = new List<Models.Http.Send.Variety>() 
                                    { 
                                        new Models.Http.Send.Variety()
                                        {
                                            Id = variety.ProductVariety.Id,
                                            Quantity = (double)variety.ProductVariety.ProductQuantity.Quantity,
                                            MeasurementUnit = variety.ProductVariety.ProductQuantity.QuantityMeasurementUnit.Name,
                                            Price = (double)variety.ProductVariety.Price
                                        }
                                    },
                                }).ToList(),
                    Operations = (from history in item.OrderHistory
                                  select new Operation()
                                  {
                                      Id = history.Id,
                                      Name = history.Operation.Name,
                                      Date = history.Date,
                                      Time = history.Time.ToString(),
                                      Status = new Status()
                                      {
                                          Id = history.OrderStatus.Id,
                                          Name = history.OrderStatus.Name
                                      },
                                      Description = history.Description
                                  }).ToList()
                });

                result = new
                {
                    Status = Status,
                    Receiving = Receiving,
                    Order = Order,
                };
            });

            return result;
        }

        [Authorize]
        [HttpPost("order/place:client={IdUserClient}&receiving={IdReceiving}&building={IdBuilding}&data={Data}")]
        public async Task<ActionResult<bool>> PlaceOrder(int IdUserClient, int IdReceiving, int IdBuilding, string Data)
        {
            bool result = await Task.Run(() => {
                try
                {
                    int idUserClient = _context.Client.FirstOrDefault(client=>client.IdUser == IdUserClient).IdUser;

                    var order = _context.Order.Add(new PizzeriaDatabaze.Models.Entity.Order()
                    {
                        IdUser = IdUserClient == 0 ? 1005 : IdUserClient,
                        RegistrationDate = DateTime.Now,
                        RegistrationTime = DateTime.Now.TimeOfDay,
                        ReceivingDate = DateTime.MinValue,
                        ReceivingTime = DateTime.MinValue.TimeOfDay,
                        IdBuilding = IdBuilding,
                        IdReceivingMethod = IdReceiving,
                        TotalPrice = 0,
                        IdStatus = 1,
                        IsDeleted = false
                    });



                    List<PizzeriaDatabaze.Models.Entity.OrderIncludeProductVariety> content =
                        (from item in Data.Split('+')
                         select new PizzeriaDatabaze.Models.Entity.OrderIncludeProductVariety()
                         {
                             IdOrder = order.Id,
                             IdProductVariety = int.Parse(item.Split('_')[0]),
                             Amount = int.Parse(item.Split('_')[1]),
                             IsDeleted = false
                         }).ToList();

                    decimal total = 0;
                    foreach (PizzeriaDatabaze.Models.Entity.OrderIncludeProductVariety item in content)
                    {
                        total += _context.ProductVariety.FirstOrDefault(variety => variety.Id == item.IdProductVariety).Price;
                        order.OrderIncludeProductVariety.Add(item);
                    }

                    order.TotalPrice = total;

                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    return false;
                }

                return true;

            });

            return result;
        }
    }
}
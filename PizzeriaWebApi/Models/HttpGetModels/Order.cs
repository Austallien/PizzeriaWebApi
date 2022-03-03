using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.HttpGetModels
{
    public class Order
    {
        public int Id { get; set; }
        public long RegistrationDate { get; set; }
        public long RegistrationTime { get; set; }
        public long ReceivingDate { get; set; }
        public long ReceivingTime { get; set; }
        public string ReceivingMethod { get; set; }
        public string Address { get; set; }
        public string DeliveryAddress { get; set; }
        public double TotalPrice { get; set; }
        public string Status { get; set; }

        /*public static Order ConvertFromDbOrder(Models.EntityModels.Order Order)
        {
            Order order = new Order()
            {
                Id = Order.Id,
                RegistrationDate = (long)Order.RegistrationDate.Date.Subtract(new DateTime(1970, 1, 1)).TotalSeconds * 1000,
                RegistrationTime = Order.RegistrationTime.Ticks,
                ReceivingDate = Order.ReceivingDate.Date.Ticks,
                ReceivingTime = Order.ReceivingTime.Ticks,
                ReceivingMethod = Order.ReceivingMethod.Name,
                Address = Order.IdBuilding.ToString(),
                DeliveryAddress = Order.IdBuilding.ToString(),
                TotalPrice = Convert.ToDouble(Order.TotalPrice),
                Status = Order.OrderStatus.Name
            };

            return order;
        }

        public static Order GetFromDatabaseByOrderId(EntityModels.PizzeriaContext Context, long IdOrder)
        {
            return Context.Order.Where(item => item.Id == IdOrder).Select(item => new Models.HttpGetModels.Order()
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
        }

        public static List<Order> GetFromDatabaseByUserId(EntityModels.PizzeriaContext Context, long IdUser)
        {
            return Context.Order.Where(item => item.IdUser == IdUser).Select(item => new Models.HttpGetModels.Order()
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
            }).ToList();
        }*/
    }
}
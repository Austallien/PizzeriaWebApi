using System;
using System.Collections.Generic;

namespace Api.Models.Http.Building.Send
{
    class Order
    {
        public int Id { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string RegistrationTime { get; set; }
        public DateTime ReceivingDate { get; set; }
        public string ReceivingTime { get; set; }
        public double Total { get; set; }
        public int IdStatus { get; set; }
        public int IdReceiving { get; set; }
        public List<Http.Send.Product> Products { get; set; }
        public List<Operation> Operations { get; set; }
    }
}
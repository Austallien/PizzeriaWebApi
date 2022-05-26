using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Models.Http.Building.Send
{
    class Order
    {
        public int Id;
        public long RegistrationDate;
        public long RegistrationTime;
        public long ReceivingDate;
        public long ReceivingTime;
        public double Total;
        public int IdStatus;
        public int IdReceiving;
    }
}
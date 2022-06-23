using System;

namespace Api.Models.Http.Building.Send
{
    class Operation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public Status Status { get; set; }
        public string Description { get; set; }
    }
}

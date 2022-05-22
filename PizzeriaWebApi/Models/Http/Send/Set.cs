using System.Collections.Generic;

namespace Api.Models.Http.Send
{
    public class Set
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> Products { get; set; }
        public bool IsDeleted { get; set; }
    }
}
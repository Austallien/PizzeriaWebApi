using System.Collections.Generic;

namespace Api.Models.Http.Send
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> Products { get; set; }
    }
}
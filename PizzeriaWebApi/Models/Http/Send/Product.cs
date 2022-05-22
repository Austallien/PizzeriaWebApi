using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;

namespace Api.Models.Http.Send
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public List<Variety> Varieties { get; set; }
        public List<string> Composition { get; set; }
        public bool IsDeleted { get; set; }
    }
}
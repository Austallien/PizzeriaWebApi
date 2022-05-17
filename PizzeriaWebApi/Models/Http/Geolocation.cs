using System.Collections.Generic;

namespace Api.Models.Http
{
    public class Geolocation
    {
        public List<Country> Countries { get; set; }

        public class Country
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public List<City> Cities { get; set; }
        }

        public class City
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public List<Street> Streets { get; set; }
        }

        public class Street
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
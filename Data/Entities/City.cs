using System;
using System.Collections.Generic;

namespace Data.Entities
{

    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int StateId { get; set; }
        public State State { get; set; }
        private List<Restaurant>? Restaurants { get; set; }
    }
}
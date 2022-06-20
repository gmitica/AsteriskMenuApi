using System;
using System.Collections.Generic;

namespace Data.Entities
{

    public class State
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        private List<City>? Cities { get; set; }
    }
}
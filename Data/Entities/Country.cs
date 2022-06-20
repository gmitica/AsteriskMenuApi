using System;
using System.Collections.Generic;

namespace Data.Entities
{

    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        private List<State>? States { get; set; }
    }
}
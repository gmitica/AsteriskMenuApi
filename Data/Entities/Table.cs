using System;
using System.Collections.Generic;

namespace Data.Entities
{

    public class Table
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public Guid RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
        
        
        public int RowOrder { get; set;  }
        public bool Active { get; set; }
        public DateTime? DateDeleted { get; set; }
        public List<Order>? Orders { get; set; }
    }
}
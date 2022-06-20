using System;
using System.Collections.Generic;

namespace Data.Entities
{

    public class Item
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public Byte[]? Image { get; set; }
        public int RowOrder { get; set;  }
        public bool Active { get; set; } = true;
        public DateTime? DateDeleted { get; set; }
        public List<RestaurantItemCategory>? RestaurantItemCategories { get; set; }
    }
}
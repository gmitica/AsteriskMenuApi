using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public class Category
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public int RowOrder { get; set;  }
        public bool Active { get; set; } = true;
        public DateTime? DateDeleted { get; set; }
        public List<RestaurantItemCategory>? RestaurantItemCategories { get; set; }
    }
}
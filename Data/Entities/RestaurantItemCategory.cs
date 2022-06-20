using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public class RestaurantItemCategory
    {
        public Guid Id { get; set; }
        public Guid? CategoryId { get; set; }
        public Category? Category { get; set; }

        public Guid RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        public Guid? ItemId { get; set; }
        public Item? Item { get; set; }

        public bool Active { get; set; }
        public List<OrderItem>? OrderItems { get; set; }
    }
}
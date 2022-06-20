using System;
using Data.Entities;

namespace Data.Infrastructure
{
    public class RestaurantItemCategoryOrder
    {
        public Guid Id { get; set; }
        public Guid? CategoryId { get; set; }
        public Category? Category { get; set; }

        public Guid RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        public Guid? ItemId { get; set; }
        public Item? Item { get; set; }

        public int Units { get; set; } = 0;
    }
}
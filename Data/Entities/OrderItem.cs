using System;

namespace Data.Entities
{
    public class OrderItem
    {
        public Guid OrderId { get; set; }
        public Order? Order { get; set; }

        public Guid RestaurantItemCategoryId { get; set; }
        public RestaurantItemCategory RestaurantItemCategory { get; set; }

        public int Units { get; set; }
    }
}
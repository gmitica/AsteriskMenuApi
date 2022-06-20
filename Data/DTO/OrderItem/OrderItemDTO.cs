using System;

namespace Data.DTO.OrderItem
{
    public class OrderItemDTO
    {
        public Guid OrderId { get; set; }
        public Guid RestaurantItemCategoryId { get; set; }
        public int Units { get; set; }
    }
}
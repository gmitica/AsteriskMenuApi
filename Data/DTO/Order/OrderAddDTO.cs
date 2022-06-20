using System;
using System.Collections.Generic;
using Data.DTO.OrderItem;

namespace Data.DTO.Order
{
    public class OrderAddDTO
    {
        public Guid? WaiterId { get; set; }
        public Guid TableId { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateFinish { get; set; }
        public List<OrderItemDTO>? OrderItems { get; set; }
    }
}
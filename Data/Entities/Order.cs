using System;
using System.Collections.Generic;

namespace Data.Entities
{

    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid? WaiterId { get; set; }
        public User? Waiter { get; set; }
        public Guid? TableId { get; set; }
        public Table? Table { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateFinish { get; set; }
        public List<OrderItem>? OrderItems { get; set; }
        
        
    }
}
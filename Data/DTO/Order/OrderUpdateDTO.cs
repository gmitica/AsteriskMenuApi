using System;

namespace Data.DTO.Order
{
    public class OrderUpdateDTO
    {
        public Guid Id { get; set; }
        public Guid? WaiterId { get; set; }
        public Guid? TableId { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateFinish { get; set; }
    }
}
using System;
namespace Data.DTO.Category
{
    public class CategoryAddDTO
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public int RowOrder { get; set;  }
        public bool Active { get; set; } = true;
        public Guid RestaurantId { get; set; }
    }
}
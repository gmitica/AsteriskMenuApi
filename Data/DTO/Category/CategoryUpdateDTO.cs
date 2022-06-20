using System;
namespace Data.DTO.Category
{
    public class CategoryUpdateDTO
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public int RowOrder { get; set;  }
    }
}
using System;
using System.Collections.Generic;
using Data.DTO.RestaurantItemCategory;
using Data.Entities;

namespace Data.DTO.Item
{
    public class ItemAddDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int RowOrder { get; set;  }
        public bool Active { get; set; } = true;
        public Byte[]? Image { get; set; }

        public Guid RestaurantId { get; set; }

    }
}
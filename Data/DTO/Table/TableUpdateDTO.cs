using System;
using System.Collections.Generic;
using Data.Entities;

namespace Data.DTO.Table
{
    public class TableUpdateDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public Guid RestaurantId { get; set; }
        public int RowOrder { get; set;  }
        public bool Active { get; set; }
    }
}
using System;
using System.Collections.Generic;
using Data.Entities;

namespace Data.DTO.Table
{
    public class TableAddDTO
    {
        public string Name { get; set; }
        public Guid RestaurantId { get; set; }
        public int RowOrder { get; set;  }
    }
}
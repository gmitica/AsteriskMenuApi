using System;
using System.Collections.Generic;
using Data.Entities;

namespace Data.DTO.RestaurantItemCategory
{
    public class RestaurantItemCategoryAddUpdateDTO
    {
        public Guid Id { get; set; } =  Guid.NewGuid();
        public Guid? CategoryId { get; set; }
        public Guid RestaurantId { get; set; }
        public Guid? ItemId { get; set; }
        public bool Active { get; set; } = true;
    }
}
using System;

namespace Data.DTO.Restaurant
{

    public class RestaurantAddDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public int CityId { get; set; }
        public string Address { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
        public string? PublicPhone { get; set; }
        public string? PublicEmail { get; set; }
        public string? WebSite { get; set; }
        public string Description { get; set; }
        public Byte[]? Image { get; set; }
        public int RowOrder { get; set; } = 0;
        public bool Active { get; set; } = true;
    }
}
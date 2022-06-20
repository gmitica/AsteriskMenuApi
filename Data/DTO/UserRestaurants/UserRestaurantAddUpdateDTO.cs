using System;

namespace Data.DTO.UserRestaurants
{
    public class UserRestaurantAddUpdateDTO
    {
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public Guid RestaurantId { get; set; }
        public Guid UserId { get; set; } = Guid.NewGuid();
    }
}
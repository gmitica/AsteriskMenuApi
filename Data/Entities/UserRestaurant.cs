using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Enum;

namespace Data.Entities{

    public class  UserRestaurant
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        
        public Guid RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
        
        
        public UserRestaurantType Role { get; set; } = UserRestaurantType.Waiter;
        
        public int RowOrder { get; set;  }
    }
}
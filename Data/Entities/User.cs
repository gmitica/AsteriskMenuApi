using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Data.Entities
{

    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        
        [JsonIgnore]
        public string Password { get; set; }
        [JsonIgnore]
        public Guid? TokenVerification { get; set; }
        [JsonIgnore]
        public DateTime? TokenVerificationExpire { get; set; }
        public bool isActive { get; set; } = false;
        [JsonIgnore]
        public Guid? TokenReset { get; set; }
        [JsonIgnore]
        public DateTime? TokenResetExpire { get; set; }
        
        private List<Order>? Orders { get; set; }
        [JsonIgnore]
        public List<RefreshToken>? RefreshTokens { get; set; }
        public List<UserRestaurant>? UserRestaurants { get; set; }
        public bool IsManaged { get; set; } = false;
    }
}
using System;
using System.Collections.Generic;
using Data.Entities;

namespace WebApi.Services.UserRestaurantsService
{
    public interface IUserRestaurantsService
    {
        public UserRestaurant Add(UserRestaurant userRestaurant, User connectedUser);
        public List<UserRestaurant> GetAll(Guid restaurantId, User connectedUser);
        public bool Update(UserRestaurant userRestaurant, User connectedUser);
        public bool Delete(UserRestaurant userRestaurant, User connectedUser);
    }
}
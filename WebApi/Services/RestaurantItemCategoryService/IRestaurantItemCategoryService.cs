using System;
using System.Collections.Generic;
using Data.Entities;

namespace WebApi.Services.RestaurantItemCategoryService
{
    public interface IRestaurantItemCategoryService
    {
        public bool AddUpdate(RestaurantItemCategory restaurantItemCategory, User connectedUser);
        public List<RestaurantItemCategory> GetAll(Guid restaurantId); 
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Data.Entities;
using Data.Enum;
using Microsoft.EntityFrameworkCore;
using WebApi.Data.Access;
using WebApi.Helpers;

namespace WebApi.Services.RestaurantItemCategoryService
{
    public class RestaurantItemCategoryService : IRestaurantItemCategoryService
    {
        private readonly ApplicationDbContext _dbContext;

        public RestaurantItemCategoryService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public bool AddUpdate(RestaurantItemCategory restaurantItemCategory, User connectedUser)
        {
            UserRestaurant? userRestaurant = _dbContext.UserRestaurants.FirstOrDefault(
                ur => ur.UserId == connectedUser.Id &&
                      ur.RestaurantId == restaurantItemCategory.RestaurantId && 
                      ur.Role == UserRestaurantType.Admin);
            if (userRestaurant==null)
            {
                throw new AppException("You don´t have permissions");
            }
            RestaurantItemCategory? checkCategory = _dbContext.RestaurantItemCategories.Where(x=>x.Id==restaurantItemCategory.Id).AsNoTracking().FirstOrDefault();
            if (checkCategory!=null)
            {
                _dbContext.RestaurantItemCategories.Update(restaurantItemCategory);
                return _dbContext.SaveChanges() > 0;
            }
            restaurantItemCategory.Id = Guid.NewGuid();
            _dbContext.RestaurantItemCategories.Add(restaurantItemCategory);
            return _dbContext.SaveChanges() > 0;
        }

        public List<RestaurantItemCategory> GetAll(Guid restaurantid)
        {
            return (from ric in _dbContext.RestaurantItemCategories 
                where  ric.RestaurantId==restaurantid && ric.CategoryId!=null && ric.ItemId!=null && ric.Active==true
                    select ric
                    )
                    .Include(x=>x.Category)
                    .Include(x=>x.Item)
                    .OrderBy(x=>x.Category.RowOrder)
                    .OrderBy(x=>x.Category.DisplayName)
                    .ToList();
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Data.Entities;
using Data.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WebApi.Data.Access;
using WebApi.Helpers;

namespace WebApi.Services.UserRestaurantsService
{
    public class UserRestaurantsService : IUserRestaurantsService
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRestaurantsService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public UserRestaurant Add(UserRestaurant userRestaurant, User connectedUser)
        {
            bool isAdmin = _dbContext.UserRestaurants.Any(x => 
                x.UserId == connectedUser.Id &&
                x.Role==UserRestaurantType.Admin &&
                x.RestaurantId == userRestaurant.RestaurantId);
            if (!isAdmin)
            {
                throw new AppException("You don't have permissions");
            }
            UserRestaurant result = _dbContext.UserRestaurants.Add(userRestaurant).Entity;
            _dbContext.SaveChanges();
            return result;
        }

        public List<UserRestaurant> GetAll(Guid restaurantId, User connectedUser)
        {
            bool isEnrolled = _dbContext.UserRestaurants.Any(x => x.UserId == connectedUser.Id
                                                                  && x.Role== UserRestaurantType.Admin
                                                                  && x.RestaurantId==restaurantId);
            if (!isEnrolled)
            {
                throw new AppException("You don't have permissions");
            }

            return _dbContext.UserRestaurants.Where(x => x.RestaurantId == restaurantId && x.UserId!=connectedUser.Id).Include(x=>x.User).ToList();
        }

        public bool Update(UserRestaurant userRestaurant, User connectedUser)
        {
            bool isAdmin = _dbContext.UserRestaurants.Any(x => 
                x.UserId == connectedUser.Id &&
                x.Role==UserRestaurantType.Admin &&
                x.RestaurantId == userRestaurant.RestaurantId);
            if (!isAdmin)
            {
                throw new AppException("You don't have permissions");
            }

            _dbContext.UserRestaurants.Update(userRestaurant);
            return _dbContext.SaveChanges() > 0;
        }

        public bool Delete(UserRestaurant userRestaurant, User connectedUser)
        {
            bool isAdmin = _dbContext.UserRestaurants.Any(x => 
                x.UserId == connectedUser.Id &&
                x.Role==UserRestaurantType.Admin &&
                x.RestaurantId == userRestaurant.RestaurantId);
            if (!isAdmin)
            {
                throw new AppException("You don't have permissions");
            }

            _dbContext.UserRestaurants.Remove(userRestaurant);
            return _dbContext.SaveChanges() > 0;
        }
    }
}
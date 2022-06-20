using System;
using System.Collections.Generic;
using System.Linq;
using Data.Entities;
using Data.Enum;
using Microsoft.EntityFrameworkCore;
using WebApi.Data.Access;
using WebApi.Helpers;

namespace WebApi.Services.CategroyService
{
    public class CategoryService : ICategoryService
    {
        private ApplicationDbContext _dbContext;

        public CategoryService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Add(Category category, User connectedUser)
        {
            var restaurantId = category.RestaurantItemCategories[0].RestaurantId;
            var isAdmin = _dbContext.UserRestaurants.Where(
                x=>x.UserId==connectedUser.Id &&
                   x.Role==UserRestaurantType.Admin &&
                   x.RestaurantId==restaurantId); 

            
            if (isAdmin.FirstOrDefault()==null)
            {
                throw new AppException("You don´t have permissions");
            }
            
            _dbContext.Categories.Add(category);
            return _dbContext.SaveChanges() > 0;
        }

        public List<Category> GetAll(User connectedUser, Guid restaurantId)
        {
            return (from ur in _dbContext.UserRestaurants
                join ric in _dbContext.RestaurantItemCategories on ur.RestaurantId equals ric.RestaurantId into rict
                from rictt in rict
                join c in _dbContext.Categories on rictt.CategoryId equals c.Id
                where ur.UserId==connectedUser.Id && ur.RestaurantId==restaurantId && c.DateDeleted==null
                select c).Distinct().ToList();
        }

        public bool Update(Category category, User connectedUser)
        {
            Category? categoryToUpdate = (from ur in _dbContext.UserRestaurants
                join ric in _dbContext.RestaurantItemCategories on ur.RestaurantId equals ric.RestaurantId into rict
                from rictt in rict
                join c in _dbContext.Categories on rictt.CategoryId equals c.Id
                where ur.UserId == connectedUser.Id && c.Id == category.Id && c.DateDeleted == null
                select c).AsNoTracking().FirstOrDefault();

            if (categoryToUpdate == null)
            {
                throw new AppException("You don´t have permissions");
            }
            
            category.RestaurantItemCategories = categoryToUpdate.RestaurantItemCategories;
            _dbContext.Categories.Update(category);
            return _dbContext.SaveChanges()>0;
        }

        public bool Delete(Guid categoryId, User connectedUser)
        {
            Category category = _dbContext.Categories.Where(x => x.Id == categoryId).FirstOrDefault();
            category.DateDeleted = DateTime.Now;
            return Update(category, connectedUser);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Data.Entities;
using Data.Enum;
using Microsoft.EntityFrameworkCore;
using WebApi.Data.Access;
using WebApi.Helpers;

namespace WebApi.Services.ItemService
{
    public class ItemService : IItemService
    {
        private ApplicationDbContext _dbContext;

        public ItemService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Add(Item item, User connectedUser, Guid restaurantId)
        {
            var isAdmin = _dbContext.UserRestaurants.Where(
                x=>x.UserId==connectedUser.Id &&
                   x.Role==UserRestaurantType.Admin &&
                   x.RestaurantId==restaurantId);
            
            if (isAdmin.FirstOrDefault()==null)
            {
                throw new AppException("You don´t have permissions");
            }
            
            _dbContext.Items.Add(item);
            return _dbContext.SaveChanges() > 0;
        }

        public List<Item> GetAll(User connectedUser, Guid restaurantId)
        {
            return (from ur in _dbContext.UserRestaurants
                join ric in _dbContext.RestaurantItemCategories on ur.RestaurantId equals ric.RestaurantId into rict
                from rictt in rict
                join i in _dbContext.Items on rictt.ItemId equals i.Id
                where ur.UserId==connectedUser.Id && ur.RestaurantId==restaurantId && i.DateDeleted==null
                select i).Distinct()
                .ToList();
        }

        public bool Update(Item item, User connectedUser)
        {
            Item? itemToUpdate =  (from ur in _dbContext.UserRestaurants
                join ric in _dbContext.RestaurantItemCategories on ur.RestaurantId equals ric.RestaurantId into rict
                from rictt in rict
                join i in _dbContext.Items on rictt.ItemId equals i.Id
                where ur.UserId==connectedUser.Id && i.Id==item.Id && i.DateDeleted==null
                select i).AsNoTracking().FirstOrDefault();
            
            if (itemToUpdate == null)
            {
                throw new AppException("You don´t have permissions");
            }
            
            item.RestaurantItemCategories = itemToUpdate.RestaurantItemCategories;
            _dbContext.Items.Update(item);
            return _dbContext.SaveChanges()>0;
        }

        public bool Delete(Guid itemId, User connectedUser)
        {
            Item item = _dbContext.Items.Where(x => x.Id == itemId).FirstOrDefault();
            item.DateDeleted = DateTime.Now;
            return Update(item, connectedUser);
        }
    }
}
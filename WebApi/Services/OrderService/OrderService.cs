using System;
using System.Collections.Generic;
using System.Linq;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using WebApi.Data.Access;
using WebApi.Helpers;

namespace WebApi.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Add(Order order)
        {
            _dbContext.Add(order);
            return _dbContext.SaveChanges()>0;
        }
        
        public Order Get(Guid id)
        {
            return _dbContext.Orders.Include(x=>x.OrderItems).FirstOrDefault(x=>x.Id==id);
        }

        public List<Order> GetAll(User connectedUser, Guid restaurantId)
        {
            return (from o in _dbContext.Orders
                    join t in _dbContext.Tables on o.TableId equals t.Id
                    join ur in _dbContext.UserRestaurants on t.RestaurantId equals ur.RestaurantId
                    where ur.UserId == connectedUser.Id && ur.RestaurantId == restaurantId
                        select o).Include(x=>x.Table).Include(x=>x.Waiter).Include(x=>x.OrderItems)
                .OrderByDescending(x=>x.DateCreated)
                .ToList();
        }
        
        public List<Order> GetAll(User connectedUser)
        {
            return (from o in _dbContext.Orders
                join t in _dbContext.Tables on o.TableId equals t.Id
                join ur in _dbContext.UserRestaurants on t.RestaurantId equals ur.RestaurantId
                where ur.UserId == connectedUser.Id && o.DateFinish == null
                select o)
                .Include(x=>x.Table)
                .Include(x=>x.Waiter)
                .Include(x=>x.OrderItems)
                .OrderByDescending(x=>x.DateCreated)
                .ToList();
        }

        public bool Update(Order order, User connectedUser)
        {
            Order? toUpdate =  (from o in _dbContext.Orders
                join t in _dbContext.Tables on o.TableId equals t.Id
                join ur in _dbContext.UserRestaurants on t.RestaurantId equals ur.RestaurantId
                where ur.UserId == connectedUser.Id && o.Id == order.Id
                select o).AsNoTracking().FirstOrDefault();
            if (toUpdate==null)
            {
                throw new AppException("You don't have permissions");
            }

            _dbContext.Orders.Update(order);
            return _dbContext.SaveChanges() > 0;
        }
    }
}
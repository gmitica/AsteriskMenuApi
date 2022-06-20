using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApi.Data.Access;
using WebApi.Helpers;

namespace WebApi.Services.RestaurantService
{
    public class RestaurantService : IRestaurantService
    {
        private ApplicationDbContext _dbContext { get; set; }
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public RestaurantService(ApplicationDbContext dbContext, ILogger<RestaurantService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<Restaurant> Create(Restaurant restaurant)
        {
            Restaurant result =  _dbContext.Restaurants.Add(restaurant).Entity;
            await _dbContext.SaveChangesAsync();
            return result;
        }

        public Restaurant GetById(Guid id)
        {
            User connectedUser = (User)_httpContextAccessor.HttpContext.Items["User"];
            return (from r in _dbContext.Restaurants
                join ur in _dbContext.UserRestaurants on r.Id equals ur.RestaurantId
                where r.Id==id && ur.UserId==connectedUser.Id && r.DateDeleted==null
                select r).AsNoTracking().FirstOrDefault()!;
        }

        public IEnumerable<Restaurant> GetAll()
        {
            User connectedUser = (User)_httpContextAccessor.HttpContext.Items["User"];
            return 
                    (from r in _dbContext.Restaurants
                    join ur in _dbContext.UserRestaurants on r.Id equals ur.RestaurantId
                    where ur.UserId==connectedUser.Id && r.DateDeleted==null
                    select r).Include(x=>x.City).ThenInclude(x=>x.State).ThenInclude(x=>x.Country);
        }

        public Restaurant Update(Restaurant restaurant)
        {
            //check if connected user have permision for restaurant to update
            Restaurant check = GetById(restaurant.Id);
            if (check!=null)
                new AppException("Restaurant is incorrect");
            
            Restaurant result = _dbContext.Restaurants
                .Update(restaurant).Entity;
            _dbContext.SaveChanges();           
            return result;
        }

        public bool Delete(Guid id)
        {
            //check if connected user have permision for restaurant to update
            Restaurant restaurant = GetById(id);
            if (restaurant!=null)
                new AppException("Restaurant is incorrect");
            
            restaurant.Active = false;
            restaurant.DateDeleted  = DateTime.Now;
            _dbContext.Update(restaurant);
            return _dbContext.SaveChanges()>0;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;

namespace WebApi.Services.RestaurantService
{

    public interface IRestaurantService
    {
        Task<Restaurant> Create(Restaurant restaurant);
        Restaurant GetById(Guid id);
        IEnumerable<Restaurant> GetAll();
        Restaurant Update(Restaurant restaurant);
        bool Delete(Guid id);
    }
}
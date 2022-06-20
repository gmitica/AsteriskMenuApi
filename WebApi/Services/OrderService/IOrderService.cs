using System;
using System.Collections.Generic;
using Data.Entities;

namespace WebApi.Services.OrderService
{
    public interface IOrderService
    {
        public bool Add(Order order);

        public Order Get(Guid idOrder);
        public List<Order> GetAll(User connectedUser, Guid restaurantId);
        public List<Order> GetAll(User connectedUser);

        public bool Update(Order order, User connectedUser);
    }
}
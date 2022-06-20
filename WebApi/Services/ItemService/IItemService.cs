using System;
using System.Collections.Generic;
using Data.Entities;

namespace WebApi.Services.ItemService
{
    public interface IItemService
    {
        public bool Add(Item item, User connectedUser, Guid restaurantId);
        public List<Item> GetAll(User connectedUser, Guid restaurantId);
        public bool Update(Item item, User connectedUser);
        public bool Delete(Guid idItem, User connectedUser);
    }
}
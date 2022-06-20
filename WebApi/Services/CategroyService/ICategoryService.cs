using System;
using System.Collections.Generic;
using Data.Entities;

namespace WebApi.Services.CategroyService
{
    public interface ICategoryService
    {
        public bool Add(Category category, User connectedUser);
        public List<Category> GetAll(User connectedUser, Guid restaurantId);
        public bool Update(Category category, User connectedUser);
        public bool Delete(Guid idCategory, User connectedUser);
    }
}
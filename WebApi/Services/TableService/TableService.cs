using System;
using System.Collections.Generic;
using System.Linq;
using Data.Entities;
using Data.Enum;
using Microsoft.EntityFrameworkCore;
using WebApi.Data.Access;
using WebApi.Helpers;

namespace WebApi.Services.TableService
{
    public class TableService : ITableService
    {
        private readonly ApplicationDbContext _dbContext;

        public TableService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Table Add(Table tableToAdd, User connectedUser)
        {
            bool isAdmin = _dbContext.UserRestaurants.Any(
                x=>x.UserId==connectedUser.Id &&
                            x.Role==UserRestaurantType.Admin &&
                            x.RestaurantId==tableToAdd.RestaurantId
                );
            if (!isAdmin)
            {
                throw new AppException("You don't have permissions");
            }

            Table result = _dbContext.Tables.Add(tableToAdd).Entity;
            _dbContext.SaveChanges();
            return result;
        }

        public Table GetById(Guid? tableid)
        {
            return _dbContext.Tables.Where(x=>x.Id==tableid)
                .Include(x=>x.Restaurant)
                .FirstOrDefault();
        }

        public List<Table> GetAll(Guid restaurnatId)
        {
            return _dbContext.Tables.Where(x => 
                x.RestaurantId == restaurnatId &&
                x.DateDeleted==null
            ).ToList();
        }

        public bool Update(Table tableToUpdate, User connectedUser)
        {

            bool isAdmin = _dbContext.UserRestaurants.Any(
                x=>x.UserId==connectedUser.Id &&
                   x.Role==UserRestaurantType.Admin &&
                   x.RestaurantId==tableToUpdate.RestaurantId
            );
            if (!isAdmin)
            {
                throw new AppException("You don't have permissions");
            }
            _dbContext.Tables.Update(tableToUpdate);
            return _dbContext.SaveChanges() > 0;
        }

        public bool Delete(Guid tableId, User connectedUser)
        {
            Table tableToUpdate = _dbContext.Tables.Where(x => x.Id == tableId).FirstOrDefault();
            tableToUpdate.DateDeleted = DateTime.Now;
            return Update(tableToUpdate, connectedUser);
        }
    }
}
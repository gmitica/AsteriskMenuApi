using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Data.Entities;
using Table = Data.Entities.Table;

namespace WebApi.Services.TableService
{
    public interface ITableService
    {
        public Table Add(Table tableToAdd, User connectedUser);

        public Table GetById(Guid? tableid);
        public List<Table> GetAll(Guid restaurantId);
        public bool Update(Table tableToUpdate, User connectedUser);
        public bool Delete(Guid tableId, User connectedUser);
    }
}
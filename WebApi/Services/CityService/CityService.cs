using System.Collections.Generic;
using System.Linq;
using Data.Entities;
using WebApi.Data.Access;

namespace WebApi.Services.CityService
{
    public class CityService : ICityService
    {
        private readonly ApplicationDbContext _dbContext;

        public CityService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<City> GetAllCitiesOfState(int stateId)
        {
            return _dbContext.Cities.Where(x => x.StateId == stateId).ToList().OrderBy(x=>x.Name);
        }
    }
}
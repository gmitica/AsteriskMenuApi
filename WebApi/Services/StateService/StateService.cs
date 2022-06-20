using System.Collections.Generic;
using System.Linq;
using Data.Entities;
using WebApi.Data.Access;

namespace WebApi.Services.StateService
{
    public class StateService : IStateService
    {
        private readonly ApplicationDbContext _dbContext;

        public StateService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<State> GetAllStatesOfCountry(int countryId)
        {
            return _dbContext.States.Where(x => x.CountryId == countryId).ToList().OrderBy(x=>x.Name);
        }
    }
}
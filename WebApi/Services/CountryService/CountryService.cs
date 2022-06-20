using System.Collections.Generic;
using System.Linq;
using Data.Entities;
using WebApi.Data.Access;

namespace WebApi.Services.CountryService
{
    public class CountryService : ICountryService
    {
        private readonly ApplicationDbContext _dbContext;

        public CountryService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Country> GetAll()
        {
            return _dbContext.Countries.Select(x => x).ToList().OrderBy(x=>x.Name);
        }
    }
}
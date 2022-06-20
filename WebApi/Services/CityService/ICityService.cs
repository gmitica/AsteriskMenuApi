using System.Collections.Generic;
using Data.Entities;

namespace WebApi.Services.CityService
{
    public interface ICityService
    {
        IEnumerable<City> GetAllCitiesOfState(int stateId);
    }
}
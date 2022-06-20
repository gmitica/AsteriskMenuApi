using System.Collections.Generic;
using Data.Entities;

namespace WebApi.Services.CountryService
{
    public interface ICountryService
    {
        IEnumerable<Country> GetAll();
    }
}
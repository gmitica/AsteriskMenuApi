using System.Collections.Generic;
using Data.Entities;

namespace WebApi.Services.StateService
{
    public interface IStateService
    {
        IEnumerable<State> GetAllStatesOfCountry(int countryId);
    }
}
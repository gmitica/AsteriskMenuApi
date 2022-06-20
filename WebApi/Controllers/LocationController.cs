using Microsoft.AspNetCore.Mvc;
using WebApi.Authorization;
using WebApi.Services;
using WebApi.Services.CityService;
using WebApi.Services.CountryService;
using WebApi.Services.StateService;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Api/[controller]")]
    public class LocationController : ControllerBase
    {
        private ICountryService _countryService;
        private IStateService _stateService;
        private ICityService _cityService;

        public LocationController(ICountryService countryService, IStateService stateService, ICityService cityService)
        {
            _countryService = countryService;
            _stateService = stateService;
            _cityService = cityService;
        }
        
        /// <summary>
        /// Get all countries
        /// </summary>
        [HttpGet("Countries")]
        public IActionResult GetAllCountries()
        {
            return Ok(_countryService.GetAll());
        }

        /// <summary>
        /// Get all states of country
        /// </summary>
        /// <param name="countryId">CountryId to search</param>
        /// <returns></returns>
        [HttpGet("States/{countryId}")]
        public IActionResult GetAllSatesOfCºountry(int countryId)
        {
            return Ok(_stateService.GetAllStatesOfCountry(countryId));
        }
        
        
        /// <summary>
        /// Get all cities of state
        /// </summary>
        /// <param name="stateId">StateId to seatch</param>
        /// <returns>List with founded cities</returns>
        [HttpGet("Cities/{stateId}")]
        public IActionResult GetAllCitiesOfState(int stateId)
        {
            return Ok(_cityService.GetAllCitiesOfState(stateId));
        }

    }
}
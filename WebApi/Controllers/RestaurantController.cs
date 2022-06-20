using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Data.DTO.Restaurant;
using Data.Entities;
using Data.Enum;
using Microsoft.AspNetCore.Mvc;
using WebApi.Authorization;
using WebApi.Services.RestaurantService;

namespace WebApi.Controllers
{

    [Authorize]
    [ApiController]
    [Route("Api/[controller]")]
    public class RestaurantController : ControllerBase
    {
        private IRestaurantService _restaurantService;
        private readonly IMapper _mapper;

        public RestaurantController(IRestaurantService restaurantService, IMapper mapper)
        {
            _restaurantService = restaurantService;
            _mapper = mapper;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(RestaurantAddDTO restaurant)
        {
            var user = (User) HttpContext.Items["User"];
            Restaurant restaurantToAdd = _mapper.Map<Restaurant>(restaurant);
            restaurantToAdd.UserRestaurants = new List<UserRestaurant>
            {
                new UserRestaurant()
                {
                    RestaurantId = restaurantToAdd.Id,
                    UserId = user.Id,
                    Role = UserRestaurantType.Admin
                }
            };
            Restaurant result = await _restaurantService.Create(restaurantToAdd);
            
            if (result==null)
                return BadRequest();
            
            return Ok(result);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            IEnumerable<Restaurant> result = _restaurantService.GetAll();
            return Ok(result);
        }

        [HttpPut("Update")]
        public IActionResult Update(Restaurant restaurant)
        {
            Restaurant result = _restaurantService.Update(restaurant);
            return Ok(result);
        }

        
        [HttpDelete("Delete/{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            bool result = _restaurantService.Delete(id);
            return Ok(result);
        }
    }
}
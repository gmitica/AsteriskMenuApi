using System;
using System.Collections.Generic;
using AutoMapper;
using Data.DTO.UserRestaurants;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using WebApi.Authorization;
using WebApi.Services.UserRestaurantsService;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Api/[controller]")]
    public class UserRestaurantsController : ControllerBase
    {
        private IUserRestaurantsService _userRestaurantsService;
        private readonly IMapper _mapper;

        public UserRestaurantsController(IUserRestaurantsService userRestaurantsService, IMapper mapper)
        {
            _userRestaurantsService = userRestaurantsService;
            _mapper = mapper;
        }

        [HttpPost("Add")]
        public IActionResult AddUserRestaurant(UserRestaurantAddUpdateDTO userRestaurantAddUpdateDto)
        {
            UserRestaurant toAdd = _mapper.Map<UserRestaurant>(userRestaurantAddUpdateDto);
            toAdd.User.Password = BCrypt.Net.BCrypt.HashPassword(toAdd.User.Password); 
            var user = (User) HttpContext.Items["User"];
            UserRestaurant result = _userRestaurantsService.Add(toAdd, user);
            return Ok(result);
        }

        [HttpGet("GetAll/{restaurantId}")]
        public IActionResult GetAll(Guid restaurantId)
        {
            var user = (User) HttpContext.Items["User"];
            List<UserRestaurant> result = _userRestaurantsService.GetAll(restaurantId, user);
            return Ok(result);
        }
        
        
        [HttpPut("Update")]
        public IActionResult Update(UserRestaurantAddUpdateDTO userRestaurantAddUpdateDto)
        {
            UserRestaurant toAdd = _mapper.Map<UserRestaurant>(userRestaurantAddUpdateDto);
            toAdd.User.Password = BCrypt.Net.BCrypt.HashPassword(toAdd.User.Password); 
            var user = (User) HttpContext.Items["User"];
            bool result = _userRestaurantsService.Update(toAdd, user);
            return Ok(result);
        }
       
    }
}
using System;
using AutoMapper;
using Data.DTO.RestaurantItemCategory;
using Data.Entities;
using WebApi.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services.RestaurantItemCategoryService;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Api/[controller]")]
    public class RestaurantItemCategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRestaurantItemCategoryService _restaurantItemCategoryService;

        public RestaurantItemCategoryController(IMapper mapper, IRestaurantItemCategoryService restaurantItemCategoryService)
        {
            _mapper = mapper;
            _restaurantItemCategoryService = restaurantItemCategoryService;
        }

        [HttpPost("AddUpdate")]
        public IActionResult Add(RestaurantItemCategoryAddUpdateDTO restaurantItemCategoryAddUpdateDto)
        {
            User connectedUser = (User) HttpContext.Items["User"];
            RestaurantItemCategory restaurantItemCategory = _mapper.Map<RestaurantItemCategory>(restaurantItemCategoryAddUpdateDto);
            bool result = _restaurantItemCategoryService.AddUpdate(restaurantItemCategory, connectedUser);
            return Ok(result);
        }
        [AllowAnonymous] 
        [HttpGet("GetAll/{restaurantId}")]
        public IActionResult GetAll(Guid restaurantId)
        {
            return Ok(_restaurantItemCategoryService.GetAll(restaurantId));
        }
    }
}
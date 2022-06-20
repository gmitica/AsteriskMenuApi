using System;
using AutoMapper;
using Data.DTO.Item;
using Data.Entities;
using WebApi.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services.ItemService;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Api/[controller]")]
    public class ItemController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IItemService _itemService;

        public ItemController(IMapper mapper, IItemService itemService)
        {
            _mapper = mapper;
            _itemService = itemService;
        }

        [HttpPost("Add")]
        public IActionResult Add(ItemAddDTO itemAddDto)
        {
            User connectedUser = (User) HttpContext.Items["User"];
            Item item = _mapper.Map<Item>(itemAddDto);
            bool result = _itemService.Add(item, connectedUser, itemAddDto.RestaurantId);
            return Ok(result);
        }

        [HttpGet("GetAll/{restaurantId}")]
        public IActionResult GetAll(Guid restaurantId)
        {
            User connectedUser = (User) HttpContext.Items["User"];
            return Ok(_itemService.GetAll(connectedUser, restaurantId));
        }

        [HttpPut("Update")]
        public IActionResult Update(ItemUpdateDTO itemUpdateDto)
        {
            User connectedUser = (User) HttpContext.Items["User"];
            return Ok(_itemService.Update(_mapper.Map<Item>(itemUpdateDto), connectedUser));
        }
        
        [HttpDelete("Delete/{itemId}")]
        public IActionResult Delete(Guid itemId)
        {
            User connectedUser = (User) HttpContext.Items["User"];
            return Ok(_itemService.Delete(itemId, connectedUser));
        }

    }
}
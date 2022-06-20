using System;
using AutoMapper;
using Data.DTO.Category;
using Data.Entities;
using WebApi.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services.CategroyService;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Api/[controller]")]
    public class CategoryController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;

        public CategoryController(IMapper mapper, ICategoryService categoryService)
        {
            _mapper = mapper;
            _categoryService = categoryService;
        }

        [HttpPost("Add")]
        public IActionResult Add(CategoryAddDTO categoryAddDto)
        {
            User connectedUser = (User) HttpContext.Items["User"];
            Category category = _mapper.Map<Category>(categoryAddDto);
            bool result = _categoryService.Add(category, connectedUser);
            return Ok(result);
        }

        [HttpGet("GetAll/{restaurantId}")]
        public IActionResult GetAll(Guid restaurantId)
        {
            User connectedUser = (User) HttpContext.Items["User"];
            return Ok(_categoryService.GetAll(connectedUser, restaurantId));
        }

        [HttpPut("Update")]
        public IActionResult Update(CategoryUpdateDTO categoryUpdateDto)
        {
            User connectedUser = (User) HttpContext.Items["User"];
            return Ok(_categoryService.Update(_mapper.Map<Category>(categoryUpdateDto), connectedUser));
        }
        
        [HttpDelete("Delete/{categoryId}")]
        public IActionResult Delete(Guid categoryId)
        {
            User connectedUser = (User) HttpContext.Items["User"];
            return Ok(_categoryService.Delete(categoryId, connectedUser));
        }

    }
}
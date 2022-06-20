using System;
using System.Collections.Generic;
using AutoMapper;
using Data.DTO.Table;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using WebApi.Authorization;
using WebApi.Services.TableService;

namespace WebApi.Controllers
{
    
    [Authorize]
    [ApiController]
    [Route("Api/[controller]")]
    public class TableController: ControllerBase
    {
        private readonly ITableService _tableService;
        private readonly IMapper _mapper;

        public TableController(ITableService tableService, IMapper mapper)
        {
            _tableService = tableService;
            _mapper = mapper;
        }

        [HttpPost("Add")]
        public IActionResult Add(TableAddDTO tableAddDto)
        {
            Table tableToAdd = _mapper.Map<Table>(tableAddDto);
            tableToAdd.Id=Guid.NewGuid();
            tableToAdd.Active = true;
            User connected = (User)HttpContext.Items["User"];
            Table result = _tableService.Add(tableToAdd, connected);
            return Ok(result);
        }
        [AllowAnonymous] 
        [HttpGet("GetAll/{restaurantId}")]
        public IActionResult GetAll(Guid restaurantId)
        {
            List<Table> result = _tableService.GetAll(restaurantId);
            return Ok(result);
        }

        [HttpPut("Update")]
        public IActionResult Update(TableUpdateDTO tableUpdateDto)
        {
            Table tableToUpdate = _mapper.Map<Table>(tableUpdateDto);
            User connected = (User)HttpContext.Items["User"];
            bool result = _tableService.Update(tableToUpdate, connected);
            return Ok(result);
        }
        
        [HttpDelete("Delete/{tableId}")]
        public IActionResult Delete(Guid tableId)
        {
            User connected = (User)HttpContext.Items["User"];
            bool result = _tableService.Delete(tableId, connected);
            return Ok(result);
        }

    }
}
using System;
using AutoMapper;
using Data.DTO.Order;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using WebApi.Authorization;
using WebApi.Helpers;
using WebApi.Services.EmailService;
using WebApi.Services.OrderService;
using WebApi.Services.RestaurantService;
using WebApi.Services.TableService;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Api/[controller]")]
    public class OrderController : ControllerBase
    {
        
        
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        private readonly IEmailService _emailService;
        private readonly AppSettings _appSettings;
        private readonly ITableService _tableService;


        public OrderController(IMapper mapper, IOrderService orderService, IEmailService emailService, IOptions<AppSettings> appSettings, ITableService tableService)
        {
            _mapper = mapper;
            _orderService = orderService;
            _emailService = emailService;
            _appSettings = appSettings.Value;
            _tableService = tableService;

        }
        [AllowAnonymous] 
        [HttpPost("Add")]
        public IActionResult Add(OrderAddDTO orderAddDto)
        {
            Order order = _mapper.Map<Order>(orderAddDto);
            string email = _tableService.GetById(order.TableId).Restaurant.PublicEmail;
            _emailService.SendEmailAsync(email, "Nuevo pedido", new EmailTemplate(_appSettings).NewOrder());
            return Ok(_orderService.Add(order));
        }
        [AllowAnonymous] 
        [HttpGet("Get/{id:guid}")]
        public IActionResult Get(Guid id)
        {
            return Ok(_orderService.Get(id));
        }
        
        [HttpGet("GetAll/{restaurantId}")]
        public IActionResult GetAll(Guid restaurantId)
        {
            User connecterdUser = (User)HttpContext.Items["User"];
            return Ok(_orderService.GetAll(connecterdUser, restaurantId));
        }
        
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            User connecterdUser = (User)HttpContext.Items["User"];
            return Ok(_orderService.GetAll(connecterdUser));
        }
        
        [HttpPut("Update")]
        public IActionResult Update(OrderUpdateDTO orderUpdateDto)
        {
            Order order = _mapper.Map<Order>(orderUpdateDto);
            User connecterdUser = (User)HttpContext.Items["User"];
            return Ok(_orderService.Update(order, connecterdUser));
        }
        
    }
}
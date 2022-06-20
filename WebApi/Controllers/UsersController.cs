using System;
using AutoMapper;
using Data.DTO.Users;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Authorization;
using WebApi.Helpers;
using WebApi.Services.EmailService;
using WebApi.Services.UserService;

namespace WebApi.Controllers
{

    [Authorize]
    [ApiController]
    [Route("Api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private IEmailService _emailService;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IEmailService emailService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _emailService = emailService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("Add")]
        public IActionResult AddUser(UserAddDTO user)
        {
            User toAdd = _mapper.Map<User>(user);
            User result = _userService.AddUser(toAdd);
            _emailService.SendEmailAsync(result.Email, "Verificación de cuenta", new EmailTemplate(_appSettings).UsersVerify(result));
            return Ok("Usuario creado con exito, verificar cuenta."); 

        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate(UserAuthenticateDTO model)
        {
            User user = _mapper.Map<User>(model);
            var response = _userService.Authenticate(user, ipAddress());
            setTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("Token/Refresh")]
        public IActionResult RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = _userService.RefreshToken(refreshToken, ipAddress());
            setTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        [HttpPost("Token/Revoke")]
        public IActionResult RevokeToken(RevokeTokenRequest model)
        {
            // accept refresh token in request body or cookie
            string token = "";
            if (String.IsNullOrWhiteSpace(model.Token))
            {
                token = Request.Cookies["refreshToken"];
            }
            else
            {
                token = model.Token;
            }
            if (string.IsNullOrEmpty(token))
                return BadRequest(new {message = "Token is required"});

            _userService.RevokeToken(token, ipAddress());
            return Ok(new {message = "Token revoked"});
        }

        [AllowAnonymous]
        [HttpPost("Password/Request")]
        public IActionResult RequestPassword(string email)
        {
            Guid? token = _userService.RequestChangePassword(email);
            if (token!=null)
            {
                _emailService.SendEmailAsync(email, "Aletra de seguidad, cambio de contraseña.", new EmailTemplate(_appSettings).ResetPassword(token));
                return Ok("The mail to reset password is sent");
            }
            return BadRequest();
        }


        // helper methods

        private void setTokenCookie(string token)
        {
            // append cookie with refresh token to the http response
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(14)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private string ipAddress()
        {
            // get source ip address for the current request
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
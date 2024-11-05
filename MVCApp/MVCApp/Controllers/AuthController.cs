using Contracts.Services;
using Entities.Models.DTOs;
using Entities.Models.DTOs.User;
using Entities.Pagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace MVCApp.Controllers
{
    [Route("")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpGet("login", Name = "login-view")]
        public IActionResult LoginView()
        {
            return View();
        }
        [HttpPost("login", Name = "login")]
        public async Task<IActionResult> Login([FromForm] UserAuthorizationDto dto)
        {
            var token = await _authService.AuthorizeAsync(dto);

            if (token == null)
                return View("LoginView");

            Response.Cookies.Append("Bearer", token.Token.ToString(), new CookieOptions { HttpOnly = true, Expires = token.Expire, SameSite = SameSiteMode.Strict });

            return View("~/Views/Home/Index.cshtml");
        }
        [HttpGet("register", Name = "register-view")]
        public IActionResult RegisterView()
        {
            return View();
        }
        [HttpPost("register", Name = "register")]
        public async Task<IActionResult> Register([FromForm] UserRegistrationDto dto)
        {
            var isRegister = await _authService.RegisterAsync(dto, ["Admin"]);

            if (!isRegister)
                return View("RegisterView");

            return View("LoginView");
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TranslationsAdmin.Models;
using TranslationsAdmin.Services;

namespace TranslationsAdmin.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userService.GetAll());
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var token = await _userService.GetToken(username, password);

            return token != null ? Ok(token) : Unauthorized();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([Bind("Id,Username,Password")] UserModel user)
        {
            var success = await _userService.Create(user.Username, user.Password);

            return success ? Ok() : NotFound();
        }
    }
}
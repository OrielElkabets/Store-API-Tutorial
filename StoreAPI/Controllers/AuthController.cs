using ItemsServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreAPI.DTO;
using StoreServices;

namespace StoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly UsersService _userService;

        public AuthController(JwtService jwtService, UsersService userService)
        {
            _jwtService = jwtService;
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Auth([FromBody] AuthDTO userData)
        {
            var user = _userService.Auth(userData.Email, userData.Password);
            if(user is null) return Unauthorized("there is no user with this data");
            string token = _jwtService.GenerateToken(user.Id.ToString(), user.Role == "Admin");
            return Ok(token);
        }
    }
}

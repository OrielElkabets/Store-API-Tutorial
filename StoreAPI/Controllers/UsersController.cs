using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreAPI.DTO;
using StoreServices;
using StoreServices.Data.Entity;

namespace StoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _userService;
        private readonly IMapper _mapper;

        public UsersController(UsersService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userService.GetUsers();
            List<UserDTO> userDTOs = new List<UserDTO>(users.Count);

            foreach (var user in users)
            {
                UserDTO userDTO = _mapper.Map<UserDTO>(user);
                userDTOs.Add(userDTO);
            }

            return Ok(users);
        }

        [Authorize]

        [HttpGet("user/{id}")]
        public IActionResult GetUser(int id)
        {

            var user = _userService.GetUser(id);
            if (user is null) return NotFound($"there is no user with ID: {id}");
            return Ok(_mapper.Map<UserDTO>(user));
        }

        [HttpPost("user")]
        public IActionResult CreateUser([FromBody] NewUserDTO user)
        {
            UserEO newUser = _userService.CreateUser(_mapper.Map<UserEO>(user));
            return Ok(_mapper.Map<UserDTO>(newUser));
        }

        [HttpPut("user")]
        public IActionResult UpdateItem([FromBody] UserDTO user)
        {
            var updatedUser = _userService.UpdateUser(_mapper.Map<UserEO>(user));
            if (updatedUser is null) return NotFound($"there is no user with ID: {user.Id}");
            return Ok(_mapper.Map<UserDTO>(updatedUser));
        }

        [HttpDelete("user/{id}")]
        public IActionResult DeleteItem(int id)
        {
            var user = _userService.DeleteUser(id);
            if (user is null) return NotFound($"there is no user with ID: {id}");
            return Ok(_mapper.Map<UserDTO>(user));
        }
    }
}

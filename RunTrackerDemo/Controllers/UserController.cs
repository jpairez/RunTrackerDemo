using Application.Contract.Dto;
using Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace RunTrackerDemo.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase, IUserService
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<UserDto> CreateUserAsync(CreateUserDto user)
        {
            return await _userService.CreateUserAsync(user);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userService.DeleteUserAsync(id);
        }

        [HttpGet("{id}")]
        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            return await _userService.GetUserByIdAsync(id);
        }

        [HttpGet]
        public async Task<List<UserDto>> GetUsersAsync()
        {
            return await _userService.GetUsersAsync();
        }
    }
}

using Application.RunTracker.Contracts.Dto;
using Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace RunTrackerDemo.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/user")]
    [ApiVersion("1.0")]
    public class UserController : ControllerBase, IUserService
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<UserDto> CreateUserAsync(CreateUpdateUserDto input)
        {
            return await _userService.CreateUserAsync(input);
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

        [HttpGet("with-activity/{id}")]
        public async Task<UserWithRunningActivityDto> GetUserWithRunningActivitiesAsync(int id)
        {
           return await _userService.GetUserWithRunningActivitiesAsync(id);
        }

        [HttpPut]
        public async Task<UserDto> UpdateUserAsync(int id, CreateUpdateUserDto input)
        {
            return await _userService.UpdateUserAsync(id, input);
        }
    }
}

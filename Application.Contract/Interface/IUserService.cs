using Application.RunTracker.Contracts.Dto;
namespace Application.Contracts
{
    public interface IUserService
    {
        Task<List<UserDto>> GetUsersAsync();
        Task<UserDto> GetUserByIdAsync(int id);
        Task<UserDto> CreateUserAsync(CreateUpdateUserDto input);
        Task<bool> DeleteUserAsync(int id);
        Task<UserWithRunningActivityDto> GetUserWithRunningActivitiesAsync(int id);
        Task<UserDto> UpdateUserAsync(int id, CreateUpdateUserDto input);
    }
}

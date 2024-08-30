using Application.Contract.Dto;

namespace Application.Contracts
{
    public interface IUserService
    {
        Task<List<UserDto>> GetUsersAsync();
        Task<UserDto> GetUserByIdAsync(int id);
        Task<UserDto> CreateUserAsync(CreateUserDto user);
        Task<bool> DeleteUserAsync(int id);
    }
}

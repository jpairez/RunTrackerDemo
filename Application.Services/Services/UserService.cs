using Application.Contract.Dto;
using Application.Contracts;
using Application.Data.Data;
using Application.Data.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(AppDbContext context, IMapper mapper, ILogger<UserService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto input)
        {
            User user = new User
            {
                Name = input.Name,
                Weight = input.WeightInKilogram,
                Height = input.HeightInCentimeter,
                Birthdate = input.Birthdate,
                Age = input.Age,
                BMI = input.BMI

            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDto>(user);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                _logger.LogWarning($"User with ID {id} not found.");
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            UserDto userDto = new UserDto();
            if (user == null)
            {
                _logger.LogWarning($"User with ID {id} not found.");
                return userDto;
            }
            return _mapper.Map<UserDto>(user);
        }

        public async Task<List<UserDto>> GetUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return _mapper.Map<List<UserDto>>(users);
        }
    }
}

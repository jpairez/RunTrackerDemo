using Application.Contracts;
using Application.RunTracker.Contracts.Dto;
using Application.RunTracker.Data;
using Application.RunTracker.Data.Models;
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

        public async Task<UserDto> CreateUserAsync(CreateUpdateUserDto input)
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
            _logger.LogInformation($"Adding user {input.Name}.");
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Successfully added user {input.Name} with Id {user.Id}.");
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
            _logger.LogInformation($"Successfully deleted user Id {id}.");
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
            //var users = await _context.Users.
            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<UserWithRunningActivityDto> GetUserWithRunningActivitiesAsync(int id)
        {
            var userWithActivities = await _context.Users.Include(u => u.RunningActivities).FirstOrDefaultAsync(u => u.Id == id);
            return _mapper.Map<UserWithRunningActivityDto>(userWithActivities);
        }

        public async Task<UserDto> UpdateUserAsync(int id, CreateUpdateUserDto input)
        {
            var user = await _context.Users.FindAsync(id);
            UserDto userDto = new UserDto();
            if (user == null)
            {
                _logger.LogWarning($"User with ID {id} not found.");
                return userDto;
            }

            user.Name = input.Name;
            user.Weight = input.WeightInKilogram;
            user.Height = input.HeightInCentimeter;
            user.Birthdate = input.Birthdate;
            user.Age = input.Age;
            user.BMI = input.BMI;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Successfully updated user Id {id}.");
            return _mapper.Map<UserDto>(user);
        }
    }
}

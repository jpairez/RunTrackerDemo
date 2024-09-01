using Application.Contracts;
using Application.RunTracker.Contracts.Dto;
using Application.RunTracker.Data;
using Application.RunTracker.Data.Models;
using Application.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace RunTracker.Service.UnitTest
{
    public class UserServiceTests
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<UserService>> _mockLogger;
        private readonly AppDbContext _context;
        private readonly IUserService _userService;

        public UserServiceTests()
        {
            // Setting up dbcontext
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<UserService>>();

            _userService = new UserService(_context, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task CreateUserAsync_ShouldAddUser()
        {
            // Arrange
            var input = new CreateUpdateUserDto { Name = "John", WeightInKilogram = 70, HeightInCentimeter = 175, Birthdate = DateTime.Now.AddYears(-30) };
            var user = new User { Id = 1, Name = "John", Weight = 70, Height = 175, Birthdate = DateTime.Now.AddYears(-30), Age = 30, BMI = 22 };
            _mockMapper.Setup(m => m.Map<UserDto>(It.IsAny<User>())).Returns(new UserDto { Id = 1, Name = "John" });

            // Act
            var result = await _userService.CreateUserAsync(input);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John", result.Name);
            Assert.Single(_context.Users);
        }

        [Fact]
        public async Task DeleteUserAsync_UserExists_ShouldReturnTrue()
        {
            // Arrange
            var user = new User { Id = 1, Name = "Gavin" };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _userService.DeleteUserAsync(1);

            // Assert
            Assert.True(result);
            Assert.Empty(_context.Users);
        }

        [Fact]
        public async Task DeleteUserAsync_UserDoesNotExist_ShouldReturnFalse()
        {
            // Act
            var result = await _userService.DeleteUserAsync(99);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task GetUserByIdAsync_UserExists_ShouldReturnUser()
        {
            // Arrange
            var user = new User { Id = 1, Name = "Gavin" };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            _mockMapper.Setup(m => m.Map<UserDto>(It.IsAny<User>())).Returns(new UserDto { Id = 1, Name = "Gavin" });

            // Act
            var result = await _userService.GetUserByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Gavin", result.Name);
        }

        [Fact]
        public async Task GetUserByIdAsync_UserDoesNotExist_ShouldReturnEmptyUserDto()
        {
            // Act
            var result = await _userService.GetUserByIdAsync(3);

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.Name);
        }

        [Fact]
        public async Task UpdateUserAsync_UserExists_ShouldUpdateAndReturnUser()
        {
            // Arrange
            var user = new User { Id = 1, Name = "John", Weight = 70, Height = 175, Birthdate = DateTime.Now.AddYears(-30), Age = 30, BMI = 22 };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            var input = new CreateUpdateUserDto { Name = "John Updated", WeightInKilogram = 75, HeightInCentimeter = 180, Birthdate = DateTime.Now.AddYears(-25) };
            _mockMapper.Setup(m => m.Map<UserDto>(It.IsAny<User>())).Returns(new UserDto { Id = 1, Name = "John Updated" });

            // Act
            var result = await _userService.UpdateUserAsync(1, input);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John Updated", result.Name);
        }

        [Fact]
        public async Task GetUsersAsync_ShouldReturnAllUsers()
        {
            // Arrange
            var user1 = new User { Id = 1, Name = "John" };
            var user2 = new User { Id = 2, Name = "Gavin" };
            await _context.Users.AddRangeAsync(user1, user2);
            await _context.SaveChangesAsync();
            _mockMapper.Setup(m => m.Map<List<UserDto>>(It.IsAny<List<User>>())).Returns(new List<UserDto> { new UserDto { Id = 1, Name = "John" }, new UserDto { Id = 2, Name = "Gavin" } });

            // Act
            var result = await _userService.GetUsersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void CreateUpdateUserDto_ShouldCalculateBMI()
        {
            // Arrange
            var input = new CreateUpdateUserDto { Name = "John", WeightInKilogram = 70, HeightInCentimeter = 175, Birthdate = DateTime.Now.AddYears(-30) };

            // Act

            // Assert
            Assert.Equal(22, input.BMI);
        }
    }
}

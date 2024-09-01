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
    public class RunningActivityServiceTest
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<RunningActivityService>> _mockLogger;
        private readonly AppDbContext _context;
        private readonly IRunningActivityService _runningActivityService;

        public RunningActivityServiceTest()
        {
            // Setting up dbcontext
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<RunningActivityService>>();

            _runningActivityService = new RunningActivityService(_context, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task CreateRunningActivityAsync_ShouldAddRunningActivity()
        {
            // Arrange
            var input = new CreateUpdateRunningActivityDto { Location = "Araneta Street", RunStart = new DateTime(2024, 8, 31, 0, 0, 0), RunEnd = new DateTime(2024, 8, 31, 1, 0, 0), DistanceInKilometer = 5, UserId = 1 };
            var runningActivity = new RunningActivity { Id = 1, Location = "Araneta Street", RunStart = new DateTime(2024, 8, 31, 0, 0, 0), RunEnd = new DateTime(2024, 8, 31, 1, 0, 0), Distance = 5, UserId = 1 };
            _mockMapper.Setup(m => m.Map<RunningActivityDto>(It.IsAny<RunningActivity>())).Returns(new RunningActivityDto { Id = 1, Location = "Araneta Street" });

            // Act
            var result = await _runningActivityService.CreateRunningActivityAsync(input);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Araneta Street", result.Location);
            Assert.Single(_context.RunningActivities);
        }

        [Fact]
        public async Task DeleteRunningActivityAsync_RunningActivityExists_ShouldReturnTrue()
        {
            // Arrange
            var runningActivity = new RunningActivity { Id = 1, Location = "Araneta Street" };
            await _context.RunningActivities.AddAsync(runningActivity);
            await _context.SaveChangesAsync();

            // Act
            var result = await _runningActivityService.DeleteRunningActivityAsync(1);

            // Assert
            Assert.True(result);
            Assert.Empty(_context.RunningActivities);
        }

        [Fact]
        public async Task DeleteRunningActivityAsync_RunningActivityDoesNotExist_ShouldReturnFalse()
        {
            // Act
            var result = await _runningActivityService.DeleteRunningActivityAsync(3);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task UpdateRunningActivityAsync_RunningActivityExists_ShouldUpdateAndReturnRunningActivity()
        {
            // Arrange
            var runningActivity = new RunningActivity { Id = 1, Location = "Araneta Street", RunStart = new DateTime(2024, 8, 31, 0, 0, 0), RunEnd = new DateTime(2024, 8, 31, 1, 0, 0), Distance = 5, UserId = 1 };
            await _context.RunningActivities.AddAsync(runningActivity);
            await _context.SaveChangesAsync();
            var input = new CreateUpdateRunningActivityDto { Location = "Araneta Coli Street", RunStart = new DateTime(2024, 8, 31, 0, 0, 0), RunEnd = new DateTime(2024, 8, 31, 1, 0, 0), DistanceInKilometer = 8, UserId = 1 };
            _mockMapper.Setup(m => m.Map<RunningActivityDto>(It.IsAny<RunningActivity>())).Returns(new RunningActivityDto { Id = 1, Location = "Araneta Coli Street" });

            // Act
            var result = await _runningActivityService.UpdateRunningActivityAsync(1, input);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Araneta Coli Street", result.Location);
        }

        [Fact]
        public async Task GetRunningActivityAsync_ShouldReturnAllRunningActivities()
        {
            // Arrange
            var runningActivity1 = new RunningActivity { Id = 1, Location = "Araneta Street", RunStart = new DateTime(2024, 8, 31, 0, 0, 0), RunEnd = new DateTime(2024, 8, 31, 1, 0, 0), Distance = 5, UserId = 1 };
            var runningActivity2 = new RunningActivity { Id = 2, Location = "Lily Street", RunStart = new DateTime(2024, 8, 31, 0, 0, 0), RunEnd = new DateTime(2024, 8, 31, 2, 0, 0), Distance = 8, UserId = 2 };
            await _context.RunningActivities.AddRangeAsync(runningActivity1, runningActivity2);
            await _context.SaveChangesAsync();
            _mockMapper.Setup(m => m.Map<List<RunningActivityDto>>(It.IsAny<List<RunningActivity>>())).Returns(new List<RunningActivityDto> { new RunningActivityDto { Id = 1, Location = "Araneta Street" }, new RunningActivityDto { Id = 2, Location = "Lily Street" } });

            // Act
            var result = await _runningActivityService.GetRunningActivitiesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }
    }
}

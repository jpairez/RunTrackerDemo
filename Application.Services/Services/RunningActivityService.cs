using Application.RunTracker.Contracts.Dto;
using Application.Contracts;
using Application.RunTracker.Data;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Application.RunTracker.Data.Models;

namespace Application.Services
{
    public class RunningActivityService : IRunningActivityService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<RunningActivityService> _logger;

        public RunningActivityService(AppDbContext context, IMapper mapper, ILogger<RunningActivityService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<RunningActivityDto> CreateRunningActivityAsync(CreateUpdateRunningActivityDto input)
        {
            RunningActivity runningActivity = new RunningActivity
            {
                Location = input.Location,
                RunStart = input.RunStart,
                RunEnd = input.RunEnd,
                Distance = input.Distance,
                UserId = input.UserId
            };
            _logger.LogInformation($"Adding running activity for userid {input.UserId}.");
            _context.RunningActivities.Add(runningActivity);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Successfully added running activity for userid {input.UserId}.");
            return _mapper.Map<RunningActivityDto>(runningActivity);
        }

        public async Task<bool> DeleteRunningActivityAsync(int id)
        {
            var runningActivity = await _context.RunningActivities.FindAsync(id);
            if (runningActivity == null)
            {
                _logger.LogWarning($"Running Activity with ID {id} not found.");
                return false;
            }

            _context.RunningActivities.Remove(runningActivity);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Successfully deleted running activity Id {id}.");
            return true;
        }

        public async Task<List<RunningActivityDto>> GetRunningActivitiesAsync()
        {
            var runningActivities = await _context.RunningActivities.ToListAsync();
            return _mapper.Map<List<RunningActivityDto>>(runningActivities);
        }

        public async Task<RunningActivityDto> GetRunningActivityByIdAsync(int id)
        {
            var runningActivity = await _context.RunningActivities.FindAsync(id);
            RunningActivityDto runningActivityDto = new RunningActivityDto();
            if (runningActivity == null)
            {
                _logger.LogWarning($"Running Activity with ID {id} not found.");
                return runningActivityDto;
            }
            return _mapper.Map<RunningActivityDto>(runningActivity);
        }

        public async Task<RunningActivityDto> UpdateRunningActivityAsync(int id, CreateUpdateRunningActivityDto input)
        {
            var runningActivity = await _context.RunningActivities.FindAsync(id);
            RunningActivityDto runningActivityDto = new RunningActivityDto();
            if (runningActivity == null)
            {
                _logger.LogWarning($"Running activity with ID {id} not found.");
                return runningActivityDto;
            }

            runningActivity.Location = input.Location;
            runningActivity.RunStart = input.RunStart;
            runningActivity.RunEnd = input.RunEnd;
            runningActivity.Distance = input.Distance;
            runningActivity.UserId = input.UserId;

            _context.RunningActivities.Update(runningActivity);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Successfully updated running activity Id {id}.");
            return _mapper.Map<RunningActivityDto>(runningActivity);
        }
    }
}

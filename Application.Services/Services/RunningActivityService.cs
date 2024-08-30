using Application.Contract.Dto;
using Application.Contracts;
using Application.Data.Data;

namespace Application.Services
{
    public class RunningActivityService : IRunningActivityService
    {
        private readonly AppDbContext _context;

        public RunningActivityService(AppDbContext context)
        {
            _context = context;
        }

        public Task<RunningActivityDto> CreateRunningActivityAsync(RunningActivityDto runningActivityDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteRunningActivityAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<RunningActivityDto>> GetRunningActivitiesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RunningActivityDto> GetRunningActivityByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}

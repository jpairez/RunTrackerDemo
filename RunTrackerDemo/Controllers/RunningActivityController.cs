using Application.RunTracker.Contracts.Dto;
using Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace RunTrackerDemo.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/runningactivity")]
    [ApiVersion("1.0")]
    public class RunningActivityController : ControllerBase, IRunningActivityService
    {
        private readonly IRunningActivityService _runningActivityService;

        public RunningActivityController(IRunningActivityService runningActivityService)
        {
            _runningActivityService = runningActivityService;
        }

        [HttpPost]
        public async Task<RunningActivityDto> CreateRunningActivityAsync(CreateUpdateRunningActivityDto runningActivityDto)
        {
            return await _runningActivityService.CreateRunningActivityAsync(runningActivityDto);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteRunningActivityAsync(int id)
        {
            return await _runningActivityService.DeleteRunningActivityAsync(id);
        }

        [HttpGet("{id}")]
        public async Task<List<RunningActivityDto>> GetRunningActivitiesAsync()
        {
            return await _runningActivityService.GetRunningActivitiesAsync();
        }

        [HttpGet]
        public async Task<RunningActivityDto> GetRunningActivityByIdAsync(int id)
        {
            return await _runningActivityService.GetRunningActivityByIdAsync(id);
        }

        [HttpPut]
        public async Task<RunningActivityDto> UpdateRunningActivityAsync(int id, CreateUpdateRunningActivityDto input)
        {
            return await _runningActivityService.UpdateRunningActivityAsync(id, input);
        }
    }
}

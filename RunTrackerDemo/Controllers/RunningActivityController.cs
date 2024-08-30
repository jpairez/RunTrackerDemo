using Application.Contract.Dto;
using Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace RunTrackerDemo.Controllers
{
    [Route("api/runningactivity")]
    [ApiController]
    public class RunningActivityController : ControllerBase, IRunningActivityService
    {
        [HttpPost]
        public Task<RunningActivityDto> CreateRunningActivityAsync(RunningActivityDto runningActivityDto)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public Task<bool> DeleteRunningActivityAsync(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public Task<List<RunningActivityDto>> GetRunningActivitiesAsync()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public Task<RunningActivityDto> GetRunningActivityByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}

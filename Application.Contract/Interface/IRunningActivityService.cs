using Application.Contract.Dto;

namespace Application.Contracts
{
    public interface IRunningActivityService
    {
        Task<List<RunningActivityDto>> GetRunningActivitiesAsync();
        Task<RunningActivityDto> GetRunningActivityByIdAsync(int id);
        Task<RunningActivityDto> CreateRunningActivityAsync(RunningActivityDto runningActivityDto);
        Task<bool> DeleteRunningActivityAsync(int id);
    }
}

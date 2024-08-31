using Application.RunTracker.Contracts.Dto;

namespace Application.Contracts
{
    public interface IRunningActivityService
    {
        Task<List<RunningActivityDto>> GetRunningActivitiesAsync();
        Task<RunningActivityDto> GetRunningActivityByIdAsync(int id);
        Task<RunningActivityDto> CreateRunningActivityAsync(CreateUpdateRunningActivityDto input);
        Task<bool> DeleteRunningActivityAsync(int id);
        Task<RunningActivityDto> UpdateRunningActivityAsync(int id, CreateUpdateRunningActivityDto input);
    }
}

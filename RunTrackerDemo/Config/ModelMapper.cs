using Application.RunTracker.Contracts.Dto;
using Application.RunTracker.Data.Models;
using AutoMapper;

namespace RunTrackerDemo.Config
{
    public class ModelMapper : Profile
    {
        public ModelMapper()
        {
            CreateMap<User, UserDto>();
            CreateMap<RunningActivity, RunningActivityDto>();
            CreateMap<User, UserWithRunningActivityDto>();
        }
    }
}

using Application.Contract.Dto;
using Application.Data.Models;
using AutoMapper;

namespace RunTrackerDemo.Config
{
    public class ModelMapper : Profile
    {
        public ModelMapper()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<RunningActivity, RunningActivityDto>();
            CreateMap<RunningActivityDto, RunningActivity>();
        }
    }
}

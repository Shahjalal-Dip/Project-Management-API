using AutoMapper;
using ProjectManagementApp.Dtos;
using ProjectManagementApp.Models;

namespace ProjectManagementApp.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Project, ProjectDto>().ReverseMap();
            CreateMap<TaskItem, TaskDto>().ReverseMap();
        }
    }
}

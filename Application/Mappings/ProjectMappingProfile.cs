using AutoMapper;
using PMSystem.Application.DTOs.Projects;
using PMSystem.Domain.Entities;

namespace PMSystem.Application.Mappings
{
    public class ProjectMappingProfile : Profile
    {
      public  ProjectMappingProfile() {
            CreateMap<CreateProjectDto, Project>();
            CreateMap<Project, ProjectDto>();
            CreateMap<UpdateProjectDto, Project>();
        }
    }
}

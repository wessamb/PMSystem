using PMSystem.Application.DTOs.Projects;
using PMSystem.Domain.Entities;

namespace PMSystem.Application.Interface
{
    public interface IProjectService
    {
         Task<List<ProjectDto>> GetAllAsync();
         Task <ProjectDto> GetByIdAsync(Guid id);
            Task<Project> CreateProjectAsync(CreateProjectDto dto);
        Task<bool> UpdateProjectAsync(UpdateProjectDto dto);

        Task<bool> DeleteProjectAsync(Guid id);



    }
}

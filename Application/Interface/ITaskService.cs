using PMSystem.Application.DTOs.TaskItems;
using PMSystem.Domain.Entities;

namespace PMSystem.Application.Interface
{
    public interface ITaskService
    {
        Task<TaskDto> CreateAsync(CreateTaskDto dto);
        Task<TaskDto> GetByIdAsync(Guid id);
        Task<List<TaskDto>> GetAllAsync();
        Task<bool> UpdateAsync(UpdateTaskDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}

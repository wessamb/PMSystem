using AutoMapper;
using PMSystem.Application.DTOs.TaskItems;
using PMSystem.Domain.Entities;

namespace PMSystem.Application.Mappings
{
    public class TaskItemMappingProfile:Profile
    {
       public TaskItemMappingProfile() {
            CreateMap<CreateTaskDto, TaskItem>();
            CreateMap<TaskItem, TaskDto>();
            CreateMap<UpdateTaskDto, TaskItem>();

        }
    }
}

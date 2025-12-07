using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PMSystem.Application.DTOs.TaskItems;
using PMSystem.Application.Interface;
using PMSystem.Domain.Entities;
using PMSystem.Infrastructure;

namespace PMSystem.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly PMSystemDbContext _context;
        private readonly IMapper _mapper;

        public TaskService(PMSystemDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TaskDto> CreateAsync(CreateTaskDto dto)
        {
            var entity = _mapper.Map<TaskItem>(dto);
            await _context.TaskItems.AddAsync(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<TaskDto>(entity);
        }

        public async Task<TaskDto> GetByIdAsync(Guid id)
        {
            var entity = await _context.TaskItems.FindAsync(id);
            return entity == null ? null : _mapper.Map<TaskDto>(entity);
        }

        public async Task<List<TaskDto>> GetAllAsync()
        {
            var list = await _context.TaskItems.ToListAsync();
            return _mapper.Map<List<TaskDto>>(list);
        }

        public async Task<bool> UpdateAsync(UpdateTaskDto dto)
        {
            var existing = await _context.TaskItems.FindAsync(dto.Id);
            if (existing == null) return false;

            _mapper.Map(dto, existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var task = await _context.TaskItems.FindAsync(id);
            if (task == null) return false;

            _context.TaskItems.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMSystem.Application.DTOs.Projects;
using PMSystem.Application.Interface;
using PMSystem.Domain.Entities;
using PMSystem.Infrastructure;

namespace PMSystem.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly PMSystemDbContext _context;
        private readonly IMapper _mapper;
       public ProjectService(PMSystemDbContext context,IMapper mapper) {
        _context = context;
        _mapper = mapper;
        }
        public async Task<Project> CreateProjectAsync(CreateProjectDto dto)
        {

            var result = _mapper.Map<Project>(dto);
            await _context.Projects.AddAsync(result);
          await  _context.SaveChangesAsync();
            return result;
        }

        public async Task<bool> DeleteProjectAsync(Guid id)
        {
          var project=await _context.Projects.FindAsync(id);
            if (project == null) { 
            return false;
            }
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return true;


        }

        public async Task<List<ProjectDto>> GetAllAsync()
        {
            var projects = await _context.Projects.ToListAsync();
            var dtos = _mapper.Map<List<ProjectDto>>(projects);  
            return dtos;
        }

        public async Task<ProjectDto> GetByIdAsync(Guid id)
        {
            var project= await _context.Projects.FindAsync(id);
            if (project == null) {
                return null;
            }
            var dtos= _mapper.Map<ProjectDto>(project);
            return dtos;
        }

        public async Task<bool> UpdateProjectAsync(UpdateProjectDto dto)
        {
            var existingProject= await _context.Projects.FindAsync(dto.Id);
            if (existingProject == null) { 
            return false;
            }
            _mapper.Map(dto,existingProject);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMSystem.Application.DTOs.Projects;
using PMSystem.Application.Interface;
using PMSystem.Domain.Entities;

namespace PMSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController:ControllerBase
    {
        private readonly ILogger<ProjectController> _logger;
        private readonly IProjectService _projectService;
        public ProjectController(ILogger<ProjectController> logger, IProjectService projectService) {
        _logger = logger;
            _projectService = projectService;
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAll() {
           
           var result= await _projectService.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(Guid id)
        {
            
            var task = await _projectService.GetByIdAsync(id);
            if (task == null)
                return NotFound();
            return Ok(task);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProjectDto dto)
        {
            if (User.IsInRole("Employee"))
            {
                return Forbid();
            }
            var createdTask = await _projectService.CreateProjectAsync(dto);
            return Ok(createdTask);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProjectDto dto)
        {
            if (User.IsInRole("Employee"))
            {
                return Forbid();
            }
            if (id != dto.Id)
                return BadRequest("الـ ID لا يتطابق");

            var updated = await _projectService.UpdateProjectAsync(dto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (User.IsInRole("Employee"))
            {
                return Forbid();
            }
            var deleted = await _projectService.DeleteProjectAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

    }
}

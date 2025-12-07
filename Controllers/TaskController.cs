using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMSystem.Application.DTOs.TaskItems;
using PMSystem.Application.Interface;

namespace PMSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        // POST: api/task
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskDto dto)
        {
            var result = await _taskService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // GET: api/task
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            if (User.IsInRole("Employee"))
            {
                return Forbid();
            }
            var result = await _taskService.GetAllAsync();
            return Ok(result);
        }
        [Authorize]

        // GET: api/task/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _taskService.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // PUT: api/task/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTaskDto dto)
        {
            if (User.IsInRole("Employee"))
            {
                return Forbid();
            }
            if (id != dto.Id)
                return BadRequest("المعرف لا يتطابق");

            var updated = await _taskService.UpdateAsync(dto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/task/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (User.IsInRole("Employee"))
            {
                return Forbid();
            }
            var deleted = await _taskService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}

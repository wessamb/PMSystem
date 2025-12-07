using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PMSystem.ActionFilter;
using PMSystem.Application.DTOs.Companys;
using PMSystem.Application.Interface;

namespace PMSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly ILogger<CompanyController> _logger;
        public CompanyController(ICompanyService companyService,ILogger<CompanyController>logger)
        {
            _companyService = companyService;
            _logger = logger;
        }

        // POST: api/company

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Create([FromBody] CreateCompanyDto dto)
        {

            var result = await _companyService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // GET: api/company
        [HttpGet]
        [ServiceFilter(typeof(CustomResourceFilter))]
        [EnableCors("AnotherPolicy")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAll()
        {
            if (User.IsInRole("Employee")) {
                return Forbid();
            }
            var result = await _companyService.GetAllAsync();
            return Ok(result);
        }

        // GET: api/company/{id}
        [EnableCors("Policy1")]
        [HttpGet("{id}")]
        [Authorize(Roles ="Admin")]

        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _companyService.GetByIdAsync(id);
            if (result == null)
            { 
                _logger.LogWarning("Company with ID {CompanyId} not found.", id);
                return Problem(detail: $"Company with ID {id} not found.", statusCode: 404, title: "Not Found");

            }
            _logger.LogInformation("Company with ID {CompanyId} retrieved successfully.", id);
            return Ok(result);
        }

        // PUT: api/company
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] UpdateCompanyDto dto)
        {
            var updated = await _companyService.UpdateAsync(dto);
            if (!updated)
                return NotFound();

            return NoContent(); // 204
        }

        // DELETE: api/company/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _companyService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent(); // 204
        }
        [HttpGet("test-exception")] // 👈 أنشأنا مسارًا جديدًا للاختبار
        [AllowAnonymous]           // 👈 نتجاوز المصادقة لتسهيل الاختبار
        public IActionResult TestGlobalException()
        {
            // هنا نقوم بإحداث خطأ متعمد
            throw new Exception("This is a test exception to check the global handler!");
        }

    }
}

using PMSystem.Application.DTOs.Companys;

namespace PMSystem.Application.Interface
{
    public interface ICompanyService
    {
        Task<List<CompanyDto>> GetAllAsync();
        Task<CompanyDto> GetByIdAsync(Guid id);
        Task<CompanyDto> CreateAsync(CreateCompanyDto dto);
        Task<bool> UpdateAsync(UpdateCompanyDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}

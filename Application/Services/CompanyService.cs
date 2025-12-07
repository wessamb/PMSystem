using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PMSystem.Application.DTOs.Companys;
using PMSystem.Application.Interface;
using PMSystem.Domain.Entities;
using PMSystem.Infrastructure;

namespace PMSystem.Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IMapper _mapper;
        private readonly PMSystemDbContext _context;
        public CompanyService(IMapper mapper, PMSystemDbContext context) { 
        _mapper= mapper;
            _context= context;
        } 
        public async Task<CompanyDto> CreateAsync(CreateCompanyDto dto)
        {
            var mappers= _mapper.Map<Company>(dto);
            await _context.Company.AddAsync(mappers);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<CompanyDto>(mappers);
            return result;

        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var finds= await _context.Company.FindAsync(id);
            if (finds == null) return false;
           _context.Company.Remove(finds);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<List<CompanyDto>> GetAllAsync()
        {
            var result= await _context.Company.ToListAsync();
            var mappers=_mapper.Map<List<CompanyDto>>(result);
            return mappers;
        }

        public async Task<CompanyDto> GetByIdAsync(Guid id)
        {
            var result= await _context.Company.FindAsync(id);
            if (result == null)
                return null;
            var mappers = _mapper.Map<CompanyDto>(result);
            return mappers;
        }

        public async Task<bool> UpdateAsync(UpdateCompanyDto dto)
        {
            var exting = await _context.Company.FindAsync(dto.Id);
            if (exting == null) {
                return false;
            }
            _mapper.Map(dto,exting);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

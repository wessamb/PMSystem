using PMSystem.Domain.Entities;

namespace PMSystem.Application.DTOs.Companys
{
    public class CompanyDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public int UsersCount { get; set; }
        public int ProjectsCount { get; set; }
    }
}

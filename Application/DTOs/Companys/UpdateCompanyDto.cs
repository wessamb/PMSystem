namespace PMSystem.Application.DTOs.Companys
{
    public class UpdateCompanyDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}

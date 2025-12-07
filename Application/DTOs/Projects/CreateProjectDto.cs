namespace PMSystem.Application.DTOs.Projects
{
    public class CreateProjectDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Guid CompanyId { get; set; }
    }
}

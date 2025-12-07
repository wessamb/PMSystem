using PMSystem.Domain.Common;

namespace PMSystem.Domain.Entities
{
    public class Project:BaseEntity
    {
        public Project()
        {
            Items = new List<TaskItem>();
        }
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Guid CompanyId { get; set; }

        public Company Company { get; set; }

        public List<TaskItem> Items { get; set; }
    }
}

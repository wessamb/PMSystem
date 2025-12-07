using PMSystem.Domain.Common;

namespace PMSystem.Domain.Entities
{
    public class Company:BaseEntity
    {
        public Company()
        {
            Users = new List<User>();
            Projects = new List<Project>();
        }
        public string? Name {  get; set; }
        public string? Description { get; set; }

        public List<User> Users { get; set; }

        public List<Project> Projects { get; set; }

    }
}

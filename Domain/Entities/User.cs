using Microsoft.AspNetCore.Identity;
using PMSystem.Domain.Common;
using PMSystem.Domain.Enums;

namespace PMSystem.Domain.Entities
{
    public class User: IdentityUser<Guid>
    {
        public User()
        {
            TaskItems = new List<TaskItem>();
        }
        public string? Fullname { get; set; }
        

        public RoleType RoleType { get; set; }

        public Guid? CompanyId { get; set; }

        public Company Company { get; set; }

        public List<TaskItem> TaskItems { get; set; }



    }
}

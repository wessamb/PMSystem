using PMSystem.Domain.Enums;

namespace PMSystem.Application.DTOs.Users
{
    public class RegisterDto
    {
        public string UserName { get; set; }
        public string Fullname {  get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public RoleType RoleType { get; set; }
        public Guid? CompanyId { get; set; }
    }
}

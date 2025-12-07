using PMSystem.Application.DTOs.Users;

namespace PMSystem.Application.Interface
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterDto dto);
        Task<string> LoginAsync(LoginDto dto);
    }
}

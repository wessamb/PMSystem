using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PMSystem.Application.DTOs.Users;
using PMSystem.Application.Interface;
using PMSystem.Application.Settings;
using PMSystem.Domain.Entities;

namespace PMSystem.Application.Services
{
    public class AuthService:IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtSettings _jwtSettings;
        public AuthService(UserManager<User> userManager, IOptions<JwtSettings> jwtOptions) { 
        _jwtSettings = jwtOptions.Value;
            _userManager = userManager;
        }
        public async Task<String> RegisterAsync(RegisterDto dto) {
        var existingUser=await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null) {
                throw new Exception("هذا البريد الإلكتروني مسجل بالفعل.");
            }
            var user = new User
            {
                UserName = dto.UserName,
                Email = dto.Email,
                Fullname = dto.Fullname,
                RoleType = dto.RoleType,
                CompanyId = dto.CompanyId,


            };
            var result=await _userManager.CreateAsync(user,dto.Password);
            if (!result.Succeeded) {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"فشل إنشاء المستخدم: {errors}");
            }
            return GenerateJwtToken(user);
        }
        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
                throw new Exception("البريد الإلكتروني أو كلمة المرور غير صحيحة");

            return GenerateJwtToken(user);
        }
        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Fullname ?? ""),
                new Claim(ClaimTypes.Email, user.Email ?? ""),
                new Claim(ClaimTypes.Role, user.RoleType.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: null,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}


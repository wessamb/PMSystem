using Castle.Core.Resource;
using FluentValidation;
using PMSystem.Application.DTOs.Users;
using PMSystem.Domain.Entities;

namespace PMSystem.Application.Validators
{
    public class UserRegistrationValidator:AbstractValidator<RegisterDto>
    {
        public UserRegistrationValidator()
        {
            RuleFor(user => user.UserName)
                .NotEmpty().WithMessage("اسم المستخدم مطلوب.")
                .Length(3, 50).WithMessage("يجب أن يكون اسم المستخدم بين 3 و 50 حرفًا.");
            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("البريد الإلكتروني مطلوب.")
                .EmailAddress().WithMessage("البريد الإلكتروني غير صالح.");
            RuleFor(user => user.Fullname)
                .NotEmpty().WithMessage("الاسم الكامل مطلوب.")
                .Length(2, 100).WithMessage("يجب أن يكون الاسم الكامل بين 2 و 100 حرفًا.");
            RuleFor(user => user.RoleType)
                .IsInEnum().WithMessage("نوع الدور غير صالح.");
        }
    }
}

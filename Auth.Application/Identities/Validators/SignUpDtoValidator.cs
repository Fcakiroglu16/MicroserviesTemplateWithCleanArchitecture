using FluentValidation;
using MVC.Service.Identities;

namespace Auth.Application.Identities.Validators;

public class SignUpDtoValidator : AbstractValidator<SignUpDto>
{
    public SignUpDtoValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("kullanıcı ismi  boş olamaz")
            .NotNull().WithMessage("kullanıcı ismi boş olamaz");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("email boş olamaz")
            .NotNull().WithMessage("email boş olamaz");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("şifre alanı boş olamaz")
            .NotNull().WithMessage("şifre alanı boş olamaz")
            .MinimumLength(4).WithMessage("şifre alanı minimum 4 karakter olmalıdır");
    }
}
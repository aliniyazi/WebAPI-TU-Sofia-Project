
using FluentValidation;
using WebAPI.Messages;
using WebAPI.Requests;

namespace WebAPI.Validators
{
    public class RegisterRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.UserName)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("Username can not be null")
                .NotEmpty()
                .WithMessage("Username can not be empty")
                .MinimumLength(5)
                .WithMessage("Username can not be less than 5 characters")
                .MaximumLength(50)
                .WithMessage("Username can not be more than 50 characters");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("Email can not be null")
                .NotEmpty()
                .WithMessage("Email can not be empty")
                .Matches(GlobalConstants.EMAIL_ALLOWED_CHARACTERS)
                .WithMessage("Invalid email address");

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("Password can not be null")
                .NotEmpty()
                .WithMessage("Password can not be empty")
                .MinimumLength(10)
                .WithMessage("Password must be at least 10 characters long")
                .Matches(GlobalConstants.PASSWORD_ALLOWED_CHARACTERS)
                .WithMessage("Invalid Password");

        }
    }
}

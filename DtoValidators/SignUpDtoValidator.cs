using FluentValidation;
using TaskManagerAPI.Dto;

namespace TaskManagerAPI.DtoValidators {
    public class SignUpDtoValidator : AbstractValidator<SignUpDto> {
        public SignUpDtoValidator() {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

            RuleFor(x => x.Role)
              .NotEmpty().WithMessage("Role is required.")
              .Must(role => role.Equals("admin", StringComparison.OrdinalIgnoreCase) ||
                            role.Equals("user", StringComparison.OrdinalIgnoreCase))
              .WithMessage("Role must be either 'admin' or 'user'.");
            }
        }
    }

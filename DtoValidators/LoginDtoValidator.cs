using FluentValidation;
using TaskManagerAPI.Dto;

namespace TaskManagerAPI.DtoValidators {
    public class LoginDtoValidator : AbstractValidator<LoginDto> {
        public LoginDtoValidator() {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required")
                .Length(3, 100).WithMessage("Username must be between 3 and 100 characters");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(3).WithMessage("Password must be at least 4 characters");
            }
        }
    }

using FluentValidation;
using TaskManagerAPI.Dto;

namespace TaskManagerAPI.DtoValidators {
    public class TaskDtoValidator : AbstractValidator<TaskDto> {
        public TaskDtoValidator() {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("UserId must be greater than 0.");
            }
        }
    }

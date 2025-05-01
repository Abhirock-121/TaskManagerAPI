using FluentValidation;
using TaskManagerAPI.Dto;

namespace TaskManagerAPI.DtoValidators {
    public class TaskCommentDtoValidator : AbstractValidator<TaskCommentDto> {
        public TaskCommentDtoValidator() {
            RuleFor(x => x.CommentText)
                .NotEmpty().WithMessage("Comment text is required")
                .MaximumLength(1000).WithMessage("Comment cannot exceed 1000 characters");

            RuleFor(x => x.TaskItemId)
                .GreaterThan(0).WithMessage("TaskItemId must be greater than 0");

            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("UserId must be greater than 0");
            }
        }
    }

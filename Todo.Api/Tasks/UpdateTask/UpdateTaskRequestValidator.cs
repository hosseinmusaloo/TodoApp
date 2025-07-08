using FluentValidation;

namespace Todo.Api.Tasks.UpdateTask;

public class UpdateTaskRequestValidator : AbstractValidator<UpdateTaskRequest>
{
    public UpdateTaskRequestValidator()
    {
        RuleFor(t => t.Title)
            .NotEmpty().WithMessage("وارد کردن عنوان الزامی می باشد.")
            .MaximumLength(50).WithMessage("عنوان نمی تواند بیشتر از 50 کاراکتر باشد.");
        
        RuleFor(t => t.Description)
            .MaximumLength(200).WithMessage("توضیحات نمی تواند بیشتر از 200 کاراکتر باشد.");
    }
}
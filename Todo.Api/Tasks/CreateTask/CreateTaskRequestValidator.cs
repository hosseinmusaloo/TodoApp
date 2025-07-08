using FluentValidation;

namespace Todo.Api.Tasks.CreateTask;

public class CreateTaskRequestValidator : AbstractValidator<CreateTaskRequest>
{
    public CreateTaskRequestValidator()
    {
        RuleFor(t => t.Title)
            .NotEmpty().WithMessage("وارد کردن عنوان الزامی می باشد.")
            .MaximumLength(50).WithMessage("عنوان نمی تواند بیشتر از 50 کاراکتر باشد.");
        
        RuleFor(t => t.Description)
            .MaximumLength(200).WithMessage("توضیحات نمی تواند بیشتر از 200 کاراکتر باشد.");
        
        RuleFor(x => x.DueDate)
            .Must(date => date.Date >= DateTime.Now)
            .WithMessage("تاریخ باید از امروز به بعد انتخاب شود.");
    }
}
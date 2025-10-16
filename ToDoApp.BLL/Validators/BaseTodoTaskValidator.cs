using FluentValidation;
using ToDoApp.BLL.DTOs.ToDoTask;
using ToDoApp.BLL.Extentions;
using ToDoApp.BLL.Resorces;

namespace ToDoApp.BLL.Validators;

public class BaseTodoTaskValidator : AbstractValidator<CreateToDoTaskDto>
{
    public static readonly int MaxTitleLenght = 100;
    public static readonly int MinTitleLenght = 3;
    public static readonly int MinDescriptionLenght = 10;
    public static readonly int MaxDescriptionLenght = 1000;

    public BaseTodoTaskValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
                .WithMessage(Errors_Validation.NotEmpty.FormatWith("Title"))
            .MaximumLength(MaxTitleLenght)
                .WithMessage(Errors_Validation.MaxLenght.FormatWith("Title", MaxTitleLenght))
            .MinimumLength(MinTitleLenght)
                .WithMessage(Errors_Validation.MinLenght.FormatWith("Title", MinTitleLenght));



        RuleFor(x => x.Description)
            .MinimumLength(MinDescriptionLenght)
                .WithMessage(Errors_Validation.MinLenght.FormatWith("Description", MinDescriptionLenght))
            .MaximumLength(MaxDescriptionLenght)
                .WithMessage(Errors_Validation.MaxLenght.FormatWith("Description", MaxDescriptionLenght));

        RuleFor(x => x.DueDate)
            .NotEmpty()
                .WithMessage(Errors_Validation.NotEmpty.FormatWith("DueDate"))
            .GreaterThan(DateTime.Now)
                .WithMessage(Errors_Validation.NotNow.FormatWith("DueDate"));


    }
}

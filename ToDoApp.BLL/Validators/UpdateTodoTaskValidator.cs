

using FluentValidation;
using ToDoApp.BLL.Extentions;
using ToDoApp.BLL.Mediatr.ToDoTask.Update;
using ToDoApp.BLL.Resorces;

namespace ToDoApp.BLL.Validators
{
    public class UpdateTodoTaskValidator :AbstractValidator<UpdateToDoTaskCommand>
    {
        public UpdateTodoTaskValidator(BaseTodoTaskValidator baseTodoTaskValidator)
        {
            RuleFor(x => x.UpdateToDoTaskDto.Id)
                .GreaterThan(0)
                .WithMessage(Errors_Validation.GreateThanZero.FormatWith("Id"));
            RuleFor(x => x.UpdateToDoTaskDto.Status)
                .IsInEnum()
                .WithMessage(Errors_Validation.ShouldBeEnum.FormatWith("Status"));

            RuleFor(x => x.UpdateToDoTaskDto).SetValidator(baseTodoTaskValidator);
        }
    }
}

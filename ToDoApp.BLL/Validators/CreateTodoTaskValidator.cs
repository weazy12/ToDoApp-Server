using FluentValidation;
using ToDoApp.BLL.Mediatr.ToDoTask.Create;

namespace ToDoApp.BLL.Validators
{
    public class CreateTodoTaskValidator : AbstractValidator<CreateToDoTaskCommand>
    {
        public CreateTodoTaskValidator(BaseTodoTaskValidator baseTodoTaskValidator)
        {
            RuleFor(x => x.CreateToDoTaskDto).SetValidator(baseTodoTaskValidator);
        }
    }
}

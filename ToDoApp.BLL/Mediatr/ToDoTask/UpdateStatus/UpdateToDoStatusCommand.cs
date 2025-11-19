using FluentResults;
using MediatR;
using ToDoApp.BLL.DTOs.ToDoTask;

namespace ToDoApp.BLL.Mediatr.ToDoTask.UpdateStatus
{
    public record UpdateToDoStatusCommand(UpdateTodoStatusDto UpdateTodoStatusDto) : IRequest<Result<ToDoTaskDto>>;
}

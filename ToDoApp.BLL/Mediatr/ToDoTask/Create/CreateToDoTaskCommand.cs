using FluentResults;
using MediatR;
using ToDoApp.BLL.DTOs.ToDoTask;

namespace ToDoApp.BLL.Mediatr.ToDoTask.Create;

public record CreateToDoTaskCommand(CreateToDoTaskDto CreateToDoTaskDto) : IRequest<Result<ToDoTaskDto>>;

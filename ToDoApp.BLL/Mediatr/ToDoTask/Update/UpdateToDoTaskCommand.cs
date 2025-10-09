using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using MediatR;
using ToDoApp.BLL.DTOs.ToDoTask;

namespace ToDoApp.BLL.Mediatr.ToDoTask.Update;

public record UpdateToDoTaskCommand(UpdateToDoTaskDto UpdateToDoTaskDto) : IRequest<Result<ToDoTaskDto>>;

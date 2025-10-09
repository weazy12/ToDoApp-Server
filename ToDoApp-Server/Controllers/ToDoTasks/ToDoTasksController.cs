using Microsoft.AspNetCore.Mvc;
using ToDoApp.BLL.DTOs.ToDoTask;
using ToDoApp.BLL.Mediatr.ToDoTask.Create;
using ToDoApp.BLL.Mediatr.ToDoTask.Delete;
using ToDoApp.BLL.Mediatr.ToDoTask.GetAll;
using ToDoApp.BLL.Mediatr.ToDoTask.Update;

namespace ToDoApp.WebApi.Controllers.ToDoTasks
{
    public class ToDoTasksController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllTask()
        {
            return HandleResult(await Mediator.Send(new GetAllToDoTasksQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateToDoTaskDto createToDoTaskDto)
        {
            return HandleResult(await Mediator.Send(new CreateToDoTaskCommand(createToDoTaskDto)));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateToDoTaskDto updateToDoTaskDto)
        {
            return HandleResult(await Mediator.Send(new UpdateToDoTaskCommand(updateToDoTaskDto)));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTask([FromBody] int id)
        {
            return HandleResult(await Mediator.Send(new DeleteToDoTaskCommand(id)));
        }
    }
}

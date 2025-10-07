using ToDoApp.DAL.Entities;
using ToDoApp.DAL.Repositories.Interfaces.Base;

namespace ToDoApp.DAL.Repositories.Interfaces.Tasks;

public interface IToDoTaskRepository : IRepositoryBase<ToDoTask>
{
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.DAL.Data;
using ToDoApp.DAL.Entities;
using ToDoApp.DAL.Repositories.Interfaces.Tasks;
using ToDoApp.DAL.Repositories.Realizations.Base;

namespace ToDoApp.DAL.Repositories.Realizations.Tasks
{
    public class ToDoTaskRepository : RepositoryBase<ToDoTask>, IToDoTaskRepository
    {
        public ToDoTaskRepository(ToDoAppDbContext toDoAppDbContext)
            :base(toDoAppDbContext) {
            
        }
    }
}

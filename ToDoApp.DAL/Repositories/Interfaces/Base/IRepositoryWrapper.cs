using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using ToDoApp.DAL.Repositories.Interfaces.Tasks;

namespace ToDoApp.DAL.Repositories.Interfaces.Base
{
    public interface IRepositoryWrapper
    {
        IToDoTaskRepository ToDoTaskRepository { get; }
        public int SaveChanges();

        public Task<int> SaveChangesAsync();

        public TransactionScope BeginTransaction();
    }
}

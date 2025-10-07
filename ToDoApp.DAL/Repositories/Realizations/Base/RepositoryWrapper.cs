using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using ToDoApp.DAL.Data;
using ToDoApp.DAL.Repositories.Interfaces.Base;
using ToDoApp.DAL.Repositories.Interfaces.Tasks;
using ToDoApp.DAL.Repositories.Realizations.Tasks;

namespace ToDoApp.DAL.Repositories.Realizations.Base
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly ToDoAppDbContext _toDoAppDbContext;

        private IToDoTaskRepository _toDoTaskRepository;

        public RepositoryWrapper(ToDoAppDbContext toDoAppDbContext)
        {
            _toDoAppDbContext = toDoAppDbContext;
        }
        public IToDoTaskRepository ToDoTaskRepository
        {
            get
            {
                if(_toDoTaskRepository is null)
                {
                    _toDoTaskRepository = new ToDoTaskRepository(_toDoAppDbContext);
                }

                return _toDoTaskRepository;
            }
        }

        public int SaveChanges()
        {
            return _toDoAppDbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _toDoAppDbContext.SaveChangesAsync();
        }

        public TransactionScope BeginTransaction()
        {
            return new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        }
    }
}

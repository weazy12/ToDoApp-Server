using AutoMapper;
using FluentResults;
using MediatR;
using ToDoApp.BLL.DTOs.ToDoTask;
using ToDoApp.BLL.Extentions;
using ToDoApp.BLL.Resorces;
using ToDoApp.DAL.Repositories.Interfaces.Base;

namespace ToDoApp.BLL.Mediatr.ToDoTask.Delete
{
    public class DeleteToDoTaskHandler : IRequestHandler<DeleteToDoTaskCommand, Result<ToDoTaskDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public DeleteToDoTaskHandler(IMapper mapper, IRepositoryWrapper repositoryWrapper)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
        }
        public async Task<Result<ToDoTaskDto>> Handle(DeleteToDoTaskCommand request, CancellationToken cancellationToken)
        {
            string errorMessage;
            var entity = await _repositoryWrapper.ToDoTaskRepository.GetFirstOrDefaultAsync(t => t.Id == request.id);

            if (entity == null)
            {
                errorMessage = Errors_TodoTask.NotFoundById.FormatWith("TodoTask", request.id);
                return Result.Fail(errorMessage);
            }

            _repositoryWrapper.ToDoTaskRepository.Delete(entity);

            if(await _repositoryWrapper.SaveChangesAsync() > 0)
            {
                var dto = _mapper.Map<ToDoTaskDto>(entity);
                return Result.Ok(dto);
            }

            errorMessage = Errors_TodoTask.FailedToDelete.FormatWith("TodoTask");
            return Result.Fail("Error while delete Task.");

            
        }
    }
}

using AutoMapper;
using FluentResults;
using MediatR;
using ToDoApp.BLL.DTOs.ToDoTask;
using ToDoApp.BLL.Extentions;
using ToDoApp.BLL.Resorces;
using ToDoApp.DAL.Repositories.Interfaces.Base;

namespace ToDoApp.BLL.Mediatr.ToDoTask.GetAll
{
    public class GetAllToDoTasksHandler : IRequestHandler<GetAllToDoTasksQuery, Result<IEnumerable<ToDoTaskDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public GetAllToDoTasksHandler(IMapper mapper, IRepositoryWrapper repositoryWrapper)
        {
           _mapper = mapper;
           _repositoryWrapper = repositoryWrapper;
        }

        public async Task<Result<IEnumerable<ToDoTaskDto>>> Handle(GetAllToDoTasksQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repositoryWrapper.ToDoTaskRepository.GetAllAsync();
            if (entities == null)
            {
                string errorMessage = Errors_TodoTask.NotFoundAny.FormatWith("TodoTask");
                return Result.Fail<IEnumerable<ToDoTaskDto>>(errorMessage);
            }
            var dtos = _mapper.Map<IEnumerable<ToDoTaskDto>>(entities);

            return Result.Ok(dtos);
        }
    }
}

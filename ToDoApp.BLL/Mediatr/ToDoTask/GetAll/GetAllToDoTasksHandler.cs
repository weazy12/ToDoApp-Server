using AutoMapper;
using FluentResults;
using MediatR;
using ToDoApp.BLL.DTOs.ToDoTask;
using ToDoApp.BLL.Extentions;
using ToDoApp.BLL.Interfaces.Logging;
using ToDoApp.BLL.Resorces;
using ToDoApp.DAL.Repositories.Interfaces.Base;

namespace ToDoApp.BLL.Mediatr.ToDoTask.GetAll
{
    public class GetAllToDoTasksHandler : IRequestHandler<GetAllToDoTasksQuery, Result<IEnumerable<ToDoTaskDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _loggerService;

        public GetAllToDoTasksHandler(IMapper mapper, IRepositoryWrapper repositoryWrapper, ILoggerService loggerService)
        {
           _mapper = mapper;
           _repositoryWrapper = repositoryWrapper;
           _loggerService = loggerService;
        }

        public async Task<Result<IEnumerable<ToDoTaskDto>>> Handle(GetAllToDoTasksQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repositoryWrapper.ToDoTaskRepository.GetAllAsync();
            if (entities == null)
            {
                string errorMessage = Errors_TodoTask.NotFoundAny.FormatWith("TodoTask");
                _loggerService.LogError(request, errorMessage);
                return Result.Fail<IEnumerable<ToDoTaskDto>>(errorMessage);
            }

            _loggerService.LogInformation("Success! Return all Tasks from db");
            var dtos = _mapper.Map<IEnumerable<ToDoTaskDto>>(entities);
            return Result.Ok(dtos);
        }
    }
}

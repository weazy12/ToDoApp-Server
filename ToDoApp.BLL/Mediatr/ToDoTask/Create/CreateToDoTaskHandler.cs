using AutoMapper;
using FluentResults;
using MediatR;
using ToDoApp.BLL.DTOs.ToDoTask;
using ToDoApp.BLL.Extentions;
using ToDoApp.BLL.Interfaces.Logging;
using ToDoApp.BLL.Mediatr.ToDoTask.Create;
using ToDoApp.BLL.Resorces;
using ToDoApp.DAL.Repositories.Interfaces.Base;

namespace ToDoApp.BLL.Mediatr.Tasks.Create
{
    public class CreateToDoTaskHandler : IRequestHandler<CreateToDoTaskCommand, Result<ToDoTaskDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _loggerService;

        public CreateToDoTaskHandler(IMapper mapper, IRepositoryWrapper repositoryWrapper, ILoggerService loggerService)
        {
           _mapper = mapper;
           _repositoryWrapper = repositoryWrapper;
           _loggerService = loggerService;
        }
        public async Task<Result<ToDoTaskDto>> Handle(CreateToDoTaskCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<DAL.Entities.ToDoTask>(request.CreateToDoTaskDto);
            entity.CreatedAt = DateTime.UtcNow;
            entity.Status = DAL.Enums.Status.ToDo;
                

            await _repositoryWrapper.ToDoTaskRepository.CreateAsync(entity);

            if(await _repositoryWrapper.SaveChangesAsync() > 0)
            {
                _loggerService.LogInformation($"Success! Task was created!");
                var dto = _mapper.Map<ToDoTaskDto>(entity);
                return Result.Ok(dto);
            }

            string errorMessage = Errors_TodoTask.FailedToCreate.FormatWith("TodoTask");
            _loggerService.LogError(request, errorMessage);
            return Result.Fail(errorMessage);
        }
    }
}

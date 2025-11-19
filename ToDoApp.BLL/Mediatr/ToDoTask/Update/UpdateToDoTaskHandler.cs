using AutoMapper;
using FluentResults;
using MediatR;
using ToDoApp.BLL.DTOs.ToDoTask;
using ToDoApp.BLL.Extentions;
using ToDoApp.BLL.Interfaces.Logging;
using ToDoApp.BLL.Resorces;
using ToDoApp.DAL.Repositories.Interfaces.Base;

namespace ToDoApp.BLL.Mediatr.ToDoTask.Update
{
    public class UpdateToDoTaskHandler : IRequestHandler<UpdateToDoTaskCommand, Result<ToDoTaskDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _loggerService;

        public UpdateToDoTaskHandler(IMapper mapper, IRepositoryWrapper repositoryWrapper, ILoggerService loggerService)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
            _loggerService = loggerService;
        }

        public async Task<Result<ToDoTaskDto>> Handle(UpdateToDoTaskCommand request, CancellationToken cancellationToken)
        {
            string errorMessage;
            var entity = await _repositoryWrapper.ToDoTaskRepository.GetFirstOrDefaultAsync(t => t.Id == request.UpdateToDoTaskDto.Id);

            if (entity == null)
            {
                errorMessage = Errors_TodoTask.NotFoundById.FormatWith("TodoTask", request.UpdateToDoTaskDto.Id);
                _loggerService.LogError(request, errorMessage);
                return Result.Fail(errorMessage);
            }

            _mapper.Map(request.UpdateToDoTaskDto, entity);

            _repositoryWrapper.ToDoTaskRepository.Update(entity);

            if (await _repositoryWrapper.SaveChangesAsync() > 0)
            {
                var dto = _mapper.Map<ToDoTaskDto>(entity);
                _loggerService.LogInformation("Success! Task was updated.");
                return Result.Ok(dto);
            }
            errorMessage = Errors_TodoTask.FailedToUpdate.FormatWith("TodoTask");
            _loggerService.LogError(request, errorMessage);
            return Result.Fail(errorMessage);

        }
    }
}

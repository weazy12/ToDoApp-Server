using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using MediatR;
using ToDoApp.BLL.DTOs.ToDoTask;
using ToDoApp.BLL.Interfaces.Logging;
using ToDoApp.DAL.Repositories.Interfaces.Base;

namespace ToDoApp.BLL.Mediatr.ToDoTask.UpdateStatus
{
    public class UpdateToDoStatusHandler : IRequestHandler<UpdateToDoStatusCommand, Result<ToDoTaskDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _loggerService;
        public UpdateToDoStatusHandler(IMapper mapper, IRepositoryWrapper repositoryWrapper, ILoggerService loggerService)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
            _loggerService = loggerService;
        }
        public async Task<Result<ToDoTaskDto>> Handle(UpdateToDoStatusCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repositoryWrapper.ToDoTaskRepository.GetFirstOrDefaultAsync(t => t.Id == request.UpdateTodoStatusDto.Id);

            if (entity == null)
            {
                return Result.Fail("Fail");
            }
            
            if(request.UpdateTodoStatusDto.Status == DAL.Enums.Status.Done)
            {
                entity.CompletedAt = DateTime.UtcNow;
            }
            else
            {
                entity.CompletedAt = null;
            }

            _mapper.Map(request.UpdateTodoStatusDto, entity);
            _repositoryWrapper.ToDoTaskRepository.Update(entity);

            if (await _repositoryWrapper.SaveChangesAsync() > 0)
            {
                var dto = _mapper.Map<ToDoTaskDto>(entity);
                return Result.Ok(dto);
            }

            return Result.Fail("Fail");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using MediatR;
using ToDoApp.BLL.DTOs.ToDoTask;
using ToDoApp.DAL.Repositories.Interfaces.Base;

namespace ToDoApp.BLL.Mediatr.ToDoTask.Update
{
    public class UpdateToDoTaskHandler : IRequestHandler<UpdateToDoTaskCommand, Result<ToDoTaskDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public UpdateToDoTaskHandler(IMapper mapper, IRepositoryWrapper repositoryWrapper)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<Result<ToDoTaskDto>> Handle(UpdateToDoTaskCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repositoryWrapper.ToDoTaskRepository.GetFirstOrDefaultAsync(t => t.Id == request.UpdateToDoTaskDto.Id);

            if (entity == null)
            {
                return Result.Fail("Task Not Found");
            }

            _mapper.Map(request.UpdateToDoTaskDto, entity);

            _repositoryWrapper.ToDoTaskRepository.Update(entity);

            if (await _repositoryWrapper.SaveChangesAsync() > 0)
            {
                var dto = _mapper.Map<ToDoTaskDto>(entity);
                return Result.Ok(dto);
            }

            return Result.Fail("Fail while updating Task.");

        }
    }
}

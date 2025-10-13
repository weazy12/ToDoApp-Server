using AutoMapper;
using FluentResults;
using MediatR;
using ToDoApp.BLL.DTOs.ToDoTask;
using ToDoApp.BLL.Mediatr.ToDoTask.Create;
using ToDoApp.DAL.Repositories.Interfaces.Base;

namespace ToDoApp.BLL.Mediatr.Tasks.Create
{
    public class CreateToDoTaskHandler : IRequestHandler<CreateToDoTaskCommand, Result<ToDoTaskDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public CreateToDoTaskHandler(IMapper mapper, IRepositoryWrapper repositoryWrapper)
        {
           _mapper = mapper;
           _repositoryWrapper = repositoryWrapper;
        }
        public async Task<Result<ToDoTaskDto>> Handle(CreateToDoTaskCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<DAL.Entities.ToDoTask>(request.CreateToDoTaskDto);
            entity.CreatedAt = DateTime.UtcNow;
            entity.Status = DAL.Enums.Status.ToDo;
                

            await _repositoryWrapper.ToDoTaskRepository.CreateAsync(entity);

            if(await _repositoryWrapper.SaveChangesAsync() > 0)
            {
                var dto = _mapper.Map<ToDoTaskDto>(entity);

                return Result.Ok(dto);
            }
            return Result.Fail("Failed while creating the Task.");
        }
    }
}

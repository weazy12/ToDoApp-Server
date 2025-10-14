using System.Linq.Expressions;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using ToDoApp.BLL.DTOs.ToDoTask;
using ToDoApp.BLL.Mediatr.ToDoTask.GetAll;
using ToDoApp.DAL.Repositories.Interfaces.Base;

namespace ToDoApp.XunitTests.BLL.MediatR.GetAll
{
    public class GetAllTodoTaskHanlderTests
    {
        private readonly Mock<IRepositoryWrapper> _mockRepositoryWrapper;

        private readonly Mock<IMapper> _mockMapper;

        private readonly GetAllToDoTasksHandler _handler;
        public GetAllTodoTaskHanlderTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockRepositoryWrapper = new Mock<IRepositoryWrapper>();
            _handler = new GetAllToDoTasksHandler(_mockMapper.Object, _mockRepositoryWrapper.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnOkResult_WhenGetAllWasWorkedSuccess()
        {
            var entities = new List<DAL.Entities.ToDoTask>
            {
                new DAL.Entities.ToDoTask
                {
                    Id = 1,
                    Title = "Title",
                    Description = "Lorem ipsum",
                    DueDate = DateTime.UtcNow.AddDays(3),
                    Status = DAL.Enums.Status.ToDo,
                    CreatedAt = DateTime.UtcNow,
                },
                new DAL.Entities.ToDoTask
                {
                    Id = 2,
                    Title = "Title",
                    Description = "Lorem ipsum1",
                    DueDate = DateTime.UtcNow.AddDays(4),
                    Status = DAL.Enums.Status.ToDo,
                    CreatedAt = DateTime.UtcNow,
                }
            };

            var dtos = new List<ToDoTaskDto>
            {
                new ToDoTaskDto
                {
                    Id = 1,
                    Title = "Title",
                    Description = "Lorem ipsum",
                    DueDate = DateTime.UtcNow.AddDays(3),
                    Status = DAL.Enums.Status.ToDo,
                    CreatedAt = DateTime.UtcNow,
                },
                new ToDoTaskDto
                {
                    Id = 2,
                    Title = "Title",
                    Description = "Lorem ipsum1",
                    DueDate = DateTime.UtcNow.AddDays(4),
                    Status = DAL.Enums.Status.ToDo,
                    CreatedAt = DateTime.UtcNow,
                }
            };

            _mockRepositoryWrapper.Setup(r => r.ToDoTaskRepository.GetAllAsync(
                It.IsAny<Expression<Func<DAL.Entities.ToDoTask, bool>>>(),
                It.IsAny<Func<IQueryable<DAL.Entities.ToDoTask>, IIncludableQueryable<DAL.Entities.ToDoTask, object>>>()))
                .ReturnsAsync(entities);
            
            _mockMapper.Setup(r => r.Map<IEnumerable<ToDoTaskDto>>(entities)).Returns(dtos);

            var result = await _handler.Handle(new GetAllToDoTasksQuery(), CancellationToken.None);

            result.IsSuccess.Should().BeTrue();
                _mockRepositoryWrapper.Verify(r => r.ToDoTaskRepository.GetAllAsync(
                    It.IsAny<Expression<Func<DAL.Entities.ToDoTask, bool>>>(),
                    It.IsAny<Func<IQueryable<DAL.Entities.ToDoTask>, IIncludableQueryable<DAL.Entities.ToDoTask, object>>>()),
                    Times.Once);

            _mockMapper.Verify(m => m.Map<IEnumerable<ToDoTaskDto>>(entities), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailResult_WhenTasksNotFound()
        {

            _mockRepositoryWrapper.Setup(r => r.ToDoTaskRepository.GetAllAsync(
                It.IsAny<Expression<Func<DAL.Entities.ToDoTask, bool>>>(),
                It.IsAny<Func<IQueryable<DAL.Entities.ToDoTask>, IIncludableQueryable<DAL.Entities.ToDoTask, object>>>()))
                .ReturnsAsync((IEnumerable<DAL.Entities.ToDoTask>)null);

            var result = await _handler.Handle(new GetAllToDoTasksQuery(), CancellationToken.None);

            result.IsSuccess.Should().BeFalse();

            _mockRepositoryWrapper.Verify(r => r.ToDoTaskRepository.GetAllAsync(
                It.IsAny<Expression<Func<DAL.Entities.ToDoTask, bool>>>(),
                It.IsAny<Func<IQueryable<DAL.Entities.ToDoTask>, IIncludableQueryable<DAL.Entities.ToDoTask, object>>>()),
                Times.Once);

            _mockMapper.Verify(m => m.Map<IEnumerable<ToDoTaskDto>>(It.IsAny<IEnumerable<DAL.Entities.ToDoTask>>()), Times.Never);
        }
    }
}

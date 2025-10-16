using System.Linq.Expressions;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using ToDoApp.BLL.DTOs.ToDoTask;
using ToDoApp.BLL.Mediatr.ToDoTask.Delete;
using ToDoApp.DAL.Repositories.Interfaces.Base;

namespace ToDoApp.XunitTests.BLL.MediatR.Delete
{
    public class DeleteTodoTaskHandlerTests
    {
        private readonly Mock<IRepositoryWrapper> _mockRepositoryWrapper;

        private readonly Mock<IMapper> _mockMapper;

        private readonly DeleteToDoTaskHandler _handler;
        public DeleteTodoTaskHandlerTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockRepositoryWrapper = new Mock<IRepositoryWrapper>();
            _handler = new DeleteToDoTaskHandler(_mockMapper.Object, _mockRepositoryWrapper.Object);

        }

        [Fact]
        public async Task Handle_ShouldReturnOkResult_WhenDeleteSucess()
        {
            var entity = new DAL.Entities.ToDoTask
            {
                Id = 1,
                Title = "Title",
                Description = "Lorem ipsum",
                DueDate = DateTime.UtcNow.AddDays(3),
                Status = DAL.Enums.Status.ToDo,
            };

            var returnDto = new ToDoTaskDto
            {
                Id = 1,
                Title = "Title",
                Description = "Lorem ipsum",
                DueDate = DateTime.UtcNow.AddDays(3),
                Status = DAL.Enums.Status.ToDo
            };

            _mockRepositoryWrapper.Setup(r => r.ToDoTaskRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<DAL.Entities.ToDoTask, bool>>>(),
                It.IsAny<Func<IQueryable<DAL.Entities.ToDoTask>, IIncludableQueryable<DAL.Entities.ToDoTask, object>>>()))
                .ReturnsAsync(entity);

            _mockRepositoryWrapper.Setup(r => r.ToDoTaskRepository.Delete(entity));

            _mockRepositoryWrapper.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            _mockMapper.Setup(r => r.Map<ToDoTaskDto>(entity)).Returns(returnDto);

            var result = await _handler.Handle(new DeleteToDoTaskCommand(entity.Id), CancellationToken.None);

            result.IsSuccess.Should().BeTrue();
            _mockRepositoryWrapper.Verify(r => r.ToDoTaskRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<DAL.Entities.ToDoTask, bool>>>(),
                It.IsAny<Func<IQueryable<DAL.Entities.ToDoTask>, IIncludableQueryable<DAL.Entities.ToDoTask, object>>>()),
                Times.Once);

            _mockRepositoryWrapper.Verify(r => r.ToDoTaskRepository.Delete(entity), Times.Once);
            _mockRepositoryWrapper.Verify(r => r.SaveChangesAsync(), Times.Once);
            _mockMapper.Verify(r => r.Map<ToDoTaskDto>(entity), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnOkResult_WhenEntityNotFound()
        {
            var entity = new DAL.Entities.ToDoTask
            {
                Id = 1,
                Title = "Title",
                Description = "Lorem ipsum",
                DueDate = DateTime.UtcNow.AddDays(3),
                Status = DAL.Enums.Status.ToDo,
            };

            var returnDto = new ToDoTaskDto
            {
                Id = 1,
                Title = "Title",
                Description = "Lorem ipsum",
                DueDate = DateTime.UtcNow.AddDays(3),
                Status = DAL.Enums.Status.ToDo
            };

            _mockRepositoryWrapper.Setup(r => r.ToDoTaskRepository.GetFirstOrDefaultAsync(
               It.IsAny<Expression<Func<DAL.Entities.ToDoTask, bool>>>(),
               It.IsAny<Func<IQueryable<DAL.Entities.ToDoTask>, IIncludableQueryable<DAL.Entities.ToDoTask, object>>>()))
               .ReturnsAsync((DAL.Entities.ToDoTask)null);

            var result = await _handler.Handle(new DeleteToDoTaskCommand(entity.Id), CancellationToken.None);

            result.IsSuccess.Should().BeFalse();
            _mockRepositoryWrapper.Verify(r => r.ToDoTaskRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<DAL.Entities.ToDoTask, bool>>>(),
                It.IsAny<Func<IQueryable<DAL.Entities.ToDoTask>, IIncludableQueryable<DAL.Entities.ToDoTask, object>>>()),
                Times.Once);

            _mockRepositoryWrapper.Verify(r => r.ToDoTaskRepository.Delete(entity), Times.Never);
            _mockRepositoryWrapper.Verify(r => r.SaveChangesAsync(), Times.Never);
            _mockMapper.Verify(r => r.Map<ToDoTaskDto>(entity), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailResult_WhileSaving()
        {
            var entity = new DAL.Entities.ToDoTask
            {
                Id = 1,
                Title = "Title",
                Description = "Lorem ipsum",
                DueDate = DateTime.UtcNow.AddDays(3),
                Status = DAL.Enums.Status.ToDo,
            };

            var returnDto = new ToDoTaskDto
            {
                Id = 1,
                Title = "Title",
                Description = "Lorem ipsum",
                DueDate = DateTime.UtcNow.AddDays(3),
                Status = DAL.Enums.Status.ToDo
            };

            _mockRepositoryWrapper.Setup(r => r.ToDoTaskRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<DAL.Entities.ToDoTask, bool>>>(),
                It.IsAny<Func<IQueryable<DAL.Entities.ToDoTask>, IIncludableQueryable<DAL.Entities.ToDoTask, object>>>()))
                .ReturnsAsync(entity);

            _mockRepositoryWrapper.Setup(r => r.ToDoTaskRepository.Delete(entity));

            _mockRepositoryWrapper.Setup(r => r.SaveChangesAsync()).ReturnsAsync(0);

            var result = await _handler.Handle(new DeleteToDoTaskCommand(entity.Id), CancellationToken.None);

            result.IsSuccess.Should().BeFalse();
            _mockRepositoryWrapper.Verify(r => r.ToDoTaskRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<DAL.Entities.ToDoTask, bool>>>(),
                It.IsAny<Func<IQueryable<DAL.Entities.ToDoTask>, IIncludableQueryable<DAL.Entities.ToDoTask, object>>>()),
                Times.Once);

            _mockRepositoryWrapper.Verify(r => r.ToDoTaskRepository.Delete(entity), Times.Once);
            _mockRepositoryWrapper.Verify(r => r.SaveChangesAsync(), Times.Once);
            _mockMapper.Verify(r => r.Map<ToDoTaskDto>(entity), Times.Never);
        }
    }
}

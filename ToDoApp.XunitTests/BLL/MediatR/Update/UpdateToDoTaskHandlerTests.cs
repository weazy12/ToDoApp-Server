using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using ToDoApp.BLL.DTOs.ToDoTask;
using ToDoApp.BLL.Mediatr.ToDoTask.Update;
using ToDoApp.DAL.Repositories.Interfaces.Base;

namespace ToDoApp.XunitTests.BLL.MediatR.Update
{
    public class UpdateToDoTaskHandlerTests
    {
        private readonly Mock<IRepositoryWrapper> _mockRepositoryWrapper;

        private readonly Mock<IMapper> _mockMapper;

        private readonly UpdateToDoTaskHandler _handler;
        public UpdateToDoTaskHandlerTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockRepositoryWrapper = new Mock<IRepositoryWrapper>();
            _handler = new UpdateToDoTaskHandler(_mockMapper.Object, _mockRepositoryWrapper.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnOkResult_WhenUpdatingSuccess()
        {
            var updateDto = new UpdateToDoTaskDto
            {
                Id = 1,
                Title = "Title updated",
                Description = "Lorem ipsum updated",
                DueDate = DateTime.UtcNow.AddDays(3),
                Status = DAL.Enums.Status.ToDo,
            };

            var existingEntity = new DAL.Entities.ToDoTask
            {
                Id = 1,
                Title = "Title",
                Description = "Lorem ipsum",
                DueDate = DateTime.UtcNow.AddDays(3),
                Status = DAL.Enums.Status.ToDo
            };
            var updatedEntity = new DAL.Entities.ToDoTask
            {
                Id = 1,
                Title = "Title updated",
                Description = "Lorem ipsum updated",
                DueDate = DateTime.UtcNow.AddDays(3),
                Status = DAL.Enums.Status.ToDo,
            };

            var returnDto = new ToDoTaskDto
            {
                Id = 1,
                Title = "Title updated",
                Description = "Lorem ipsum updated",
                DueDate = DateTime.UtcNow.AddDays(3),
                Status = DAL.Enums.Status.ToDo,

            };

            _mockRepositoryWrapper.Setup(r => r.ToDoTaskRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<DAL.Entities.ToDoTask, bool>>>(),
                It.IsAny<Func<IQueryable<DAL.Entities.ToDoTask>, IIncludableQueryable<DAL.Entities.ToDoTask, object>>>()))
                .ReturnsAsync(existingEntity);

            _mockMapper.Setup(r => r.Map(updateDto, existingEntity));

            _mockRepositoryWrapper.Setup(r => r.ToDoTaskRepository.Update(existingEntity));

            _mockRepositoryWrapper.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            _mockMapper.Setup(r => r.Map(updatedEntity, returnDto));

            var result = await _handler.Handle(new UpdateToDoTaskCommand(updateDto), CancellationToken.None);

            result.IsSuccess.Should().BeTrue();
            _mockRepositoryWrapper.Verify(r => r.ToDoTaskRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<DAL.Entities.ToDoTask, bool>>>(),
                It.IsAny<Func<IQueryable<DAL.Entities.ToDoTask>, IIncludableQueryable<DAL.Entities.ToDoTask, object>>>()),
                Times.Once);

            _mockMapper.Verify(r => r.Map(updateDto, existingEntity), Times.Once);
            _mockRepositoryWrapper.Verify(r => r.ToDoTaskRepository.Update(existingEntity), Times.Once);
            _mockRepositoryWrapper.Verify(r => r.SaveChangesAsync(), Times.Once);
            _mockMapper.Verify(r => r.Map<ToDoTaskDto>(existingEntity), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailResult_WhenEntityNotFound()
        {
            var updateDto = new UpdateToDoTaskDto
            {
                Id = 1,
                Title = "Title updated",
                Description = "Lorem ipsum updated",
                DueDate = DateTime.UtcNow.AddDays(3),
                Status = DAL.Enums.Status.ToDo,
            };

            var existingEntity = new DAL.Entities.ToDoTask
            {
                Id = 1,
                Title = "Title",
                Description = "Lorem ipsum",
                DueDate = DateTime.UtcNow.AddDays(3),
                Status = DAL.Enums.Status.ToDo
            };
            var updatedEntity = new DAL.Entities.ToDoTask
            {
                Id = 1,
                Title = "Title updated",
                Description = "Lorem ipsum updated",
                DueDate = DateTime.UtcNow.AddDays(3),
                Status = DAL.Enums.Status.ToDo,
            };

            var returnDto = new ToDoTaskDto
            {
                Id = 1,
                Title = "Title updated",
                Description = "Lorem ipsum updated",
                DueDate = DateTime.UtcNow.AddDays(3),
                Status = DAL.Enums.Status.ToDo,

            };

            _mockRepositoryWrapper.Setup(r => r.ToDoTaskRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<DAL.Entities.ToDoTask, bool>>>(),
                It.IsAny<Func<IQueryable<DAL.Entities.ToDoTask>, IIncludableQueryable<DAL.Entities.ToDoTask, object>>>()))
                .ReturnsAsync((DAL.Entities.ToDoTask)null);

            var result = await _handler.Handle(new UpdateToDoTaskCommand(updateDto), CancellationToken.None);

            result.IsSuccess.Should().BeFalse();
            _mockRepositoryWrapper.Verify(r => r.ToDoTaskRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<DAL.Entities.ToDoTask, bool>>>(),
                It.IsAny<Func<IQueryable<DAL.Entities.ToDoTask>, IIncludableQueryable<DAL.Entities.ToDoTask, object>>>()),
                Times.Once);

            _mockMapper.Verify(r => r.Map(updateDto, existingEntity), Times.Never);
            _mockRepositoryWrapper.Verify(r => r.ToDoTaskRepository.Update(existingEntity), Times.Never);
            _mockRepositoryWrapper.Verify(r => r.SaveChangesAsync(), Times.Never);
            _mockMapper.Verify(r => r.Map<ToDoTaskDto>(existingEntity), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailResult_WhileSaving()
        {
            var updateDto = new UpdateToDoTaskDto
            {
                Id = 1,
                Title = "Title updated",
                Description = "Lorem ipsum updated",
                DueDate = DateTime.UtcNow.AddDays(3),
                Status = DAL.Enums.Status.ToDo,
            };

            var existingEntity = new DAL.Entities.ToDoTask
            {
                Id = 1,
                Title = "Title",
                Description = "Lorem ipsum",
                DueDate = DateTime.UtcNow.AddDays(3),
                Status = DAL.Enums.Status.ToDo
            };
            var updatedEntity = new DAL.Entities.ToDoTask
            {
                Id = 1,
                Title = "Title updated",
                Description = "Lorem ipsum updated",
                DueDate = DateTime.UtcNow.AddDays(3),
                Status = DAL.Enums.Status.ToDo,
            };

            var returnDto = new ToDoTaskDto
            {
                Id = 1,
                Title = "Title updated",
                Description = "Lorem ipsum updated",
                DueDate = DateTime.UtcNow.AddDays(3),
                Status = DAL.Enums.Status.ToDo,

            };

            _mockRepositoryWrapper.Setup(r => r.ToDoTaskRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<DAL.Entities.ToDoTask, bool>>>(),
                It.IsAny<Func<IQueryable<DAL.Entities.ToDoTask>, IIncludableQueryable<DAL.Entities.ToDoTask, object>>>()))
                .ReturnsAsync(existingEntity);

            _mockMapper.Setup(r => r.Map(updateDto, existingEntity));

            _mockRepositoryWrapper.Setup(r => r.ToDoTaskRepository.Update(existingEntity));

            _mockRepositoryWrapper.Setup(r => r.SaveChangesAsync()).ReturnsAsync(0);

            var result = await _handler.Handle(new UpdateToDoTaskCommand(updateDto), CancellationToken.None);

            result.IsSuccess.Should().BeFalse();
            _mockRepositoryWrapper.Verify(r => r.ToDoTaskRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<DAL.Entities.ToDoTask, bool>>>(),
                It.IsAny<Func<IQueryable<DAL.Entities.ToDoTask>, IIncludableQueryable<DAL.Entities.ToDoTask, object>>>()),
                Times.Once);

            _mockMapper.Verify(r => r.Map(updateDto, existingEntity), Times.Once);
            _mockRepositoryWrapper.Verify(r => r.ToDoTaskRepository.Update(existingEntity), Times.Once);
            _mockRepositoryWrapper.Verify(r => r.SaveChangesAsync(), Times.Once);
            _mockMapper.Verify(r => r.Map<ToDoTaskDto>(existingEntity), Times.Never);
        }
    }
}

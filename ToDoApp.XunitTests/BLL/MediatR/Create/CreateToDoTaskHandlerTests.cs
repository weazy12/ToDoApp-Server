using AutoMapper;
using FluentAssertions;
using Moq;
using ToDoApp.BLL.DTOs.ToDoTask;
using ToDoApp.BLL.Mediatr.Tasks.Create;
using ToDoApp.BLL.Mediatr.ToDoTask.Create;
using ToDoApp.DAL.Repositories.Interfaces.Base;

namespace ToDoApp.XunitTests.BLL.MediatR.Create
{
    public class CreateToDoTaskHandlerTests
    {
        private readonly Mock<IRepositoryWrapper> _mockRepositoryWrapper;

        private readonly Mock<IMapper> _mockMapper;

        private readonly CreateToDoTaskHandler _handler;
        public CreateToDoTaskHandlerTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockRepositoryWrapper = new Mock<IRepositoryWrapper>();

            _handler = new CreateToDoTaskHandler(_mockMapper.Object, _mockRepositoryWrapper.Object);


        }

        [Fact]
        public async Task Handle_ShouldReturnOkResult_WhenCreateSuccess()
        {
            var createDto = new CreateToDoTaskDto
            {
                Title = "Title",
                Description = "Lorem ipsum",
                DueDate = DateTime.UtcNow.AddDays(3)
            };

            var entity = new DAL.Entities.ToDoTask
            {
                Id = 1,
                Title = "Title",
                Description = "Lorem ipsum",
                DueDate = DateTime.UtcNow.AddDays(3),
                Status = DAL.Enums.Status.ToDo,
                CreatedAt = DateTime.UtcNow,
            };

            var returnDto = new ToDoTaskDto
            {
                Id = 1,
                Title = "Title",
                Description = "Lorem ipsum",
                DueDate = DateTime.UtcNow.AddDays(3),
                Status = DAL.Enums.Status.ToDo,
                CreatedAt = DateTime.UtcNow
            };

            _mockMapper.Setup(r => r.Map<DAL.Entities.ToDoTask>(createDto)).Returns(entity);

            _mockRepositoryWrapper.Setup(r => r.ToDoTaskRepository.CreateAsync(entity))
                .ReturnsAsync(entity);

            _mockRepositoryWrapper.Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(1);

            _mockMapper.Setup(r => r.Map<ToDoTaskDto>(entity)).Returns(returnDto);

            var result = await _handler.Handle(new CreateToDoTaskCommand(createDto), CancellationToken.None);

            result.IsSuccess.Should().BeTrue();
            _mockMapper.Verify(m => m.Map<DAL.Entities.ToDoTask>(createDto), Times.Once);
            _mockRepositoryWrapper.Verify(r => r.ToDoTaskRepository.CreateAsync(entity), Times.Once);
            _mockRepositoryWrapper.Verify(r => r.SaveChangesAsync(), Times.Once);
            _mockMapper.Verify(r => r.Map<ToDoTaskDto>(entity), Times.Once);
        }
        [Fact]
        public async Task Handle_ShouldReturnFailResult_WhileSaving()
        {
            var createDto = new CreateToDoTaskDto
            {
                Title = "Title",
                Description = "Lorem ipsum",
                DueDate = DateTime.UtcNow.AddDays(3)
            };

            var entity = new DAL.Entities.ToDoTask
            {
                Id = 1,
                Title = "Title",
                Description = "Lorem ipsum",
                DueDate = DateTime.UtcNow.AddDays(3),
                Status = DAL.Enums.Status.ToDo,
                CreatedAt = DateTime.UtcNow,
            };

            var returnDto = new ToDoTaskDto
            {
                Id = 1,
                Title = "Title",
                Description = "Lorem ipsum",
                DueDate = DateTime.UtcNow.AddDays(3),
                Status = DAL.Enums.Status.ToDo,
                CreatedAt = DateTime.UtcNow
            };

            _mockMapper.Setup(r => r.Map<DAL.Entities.ToDoTask>(createDto)).Returns(entity);

            _mockRepositoryWrapper.Setup(r => r.ToDoTaskRepository.CreateAsync(entity))
                .ReturnsAsync(entity);

            _mockRepositoryWrapper.Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(0);

            var result = await _handler.Handle(new CreateToDoTaskCommand(createDto), CancellationToken.None);

            result.IsSuccess.Should().BeFalse();
            _mockMapper.Verify(m => m.Map<DAL.Entities.ToDoTask>(createDto), Times.Once);
            _mockRepositoryWrapper.Verify(r => r.ToDoTaskRepository.CreateAsync(entity), Times.Once);
            _mockRepositoryWrapper.Verify(r => r.SaveChangesAsync(), Times.Once);
            _mockMapper.Verify(r => r.Map<ToDoTaskDto>(entity), Times.Never);
        }
    }
}

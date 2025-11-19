using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using ToDoApp.BLL.DTOs.ToDoTask;
using ToDoApp.BLL.Mediatr.ToDoTask.Update;
using ToDoApp.BLL.Validators;

namespace ToDoApp.XunitTests.BLL.Validator
{
    public class UpdateTodoTaskValidatorTests
    {
        private readonly Mock<BaseTodoTaskValidator> _mockBaseTodoTaskValidator;
        private readonly UpdateTodoTaskValidator _updateTodoTaskValidator;
        public UpdateTodoTaskValidatorTests()
        {
            _mockBaseTodoTaskValidator = new Mock<BaseTodoTaskValidator>();
            _updateTodoTaskValidator = new UpdateTodoTaskValidator(_mockBaseTodoTaskValidator.Object);
        }

        [Fact]
        public async Task ValidationPassSuccess_WhenCommandValid()
        {
            var dto = UpdateValidCommand();

            var result = await _updateTodoTaskValidator.ValidateAsync(dto);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task ValidatioFail_WhenIdLessThanZero()
        {
            var dto = UpdateValidCommand();

            dto.UpdateToDoTaskDto.Id = -1;

            var result = await _updateTodoTaskValidator.ValidateAsync(dto);

            result.IsValid.Should().BeFalse();
        }
        private UpdateToDoTaskCommand UpdateValidCommand()
        {
            var dto = new UpdateToDoTaskDto
            {
                Id = 1,
                Title = "Valid Task Title",
                Description = "This is a valid content with enough characters",
                DueDate = DateTime.UtcNow.AddDays(7),
            };

            return new UpdateToDoTaskCommand(dto);
        }
    }
}

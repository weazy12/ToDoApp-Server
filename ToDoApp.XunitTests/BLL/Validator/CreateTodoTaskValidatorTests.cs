using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using ToDoApp.BLL.DTOs.ToDoTask;
using ToDoApp.BLL.Mediatr.ToDoTask.Create;
using ToDoApp.BLL.Validators;

namespace ToDoApp.XunitTests.BLL.Validator
{
    public class CreateTodoTaskValidatorTests
    {
        private readonly Mock<BaseTodoTaskValidator> _mockBaseTodoTaskValidator;
        private readonly CreateTodoTaskValidator _createTodoTaskValidator;
        public CreateTodoTaskValidatorTests()
        {
            _mockBaseTodoTaskValidator = new Mock<BaseTodoTaskValidator>();
            _createTodoTaskValidator = new CreateTodoTaskValidator(_mockBaseTodoTaskValidator.Object);
        }

        [Fact]
        public async Task ValidationPassSuccess_WhenCommandValid()
        {
            var command = CreateValidCommand();

            var result = await _createTodoTaskValidator.ValidateAsync(command);

            result.IsValid.Should().BeTrue();
        }

        private CreateToDoTaskCommand CreateValidCommand()
        {
            var dto = new CreateToDoTaskDto
            {
                Title = "Valid Task",
                Description = "This is a valid description",
                DueDate = DateTime.Now.AddDays(1)
            };

            return new CreateToDoTaskCommand(dto);
        }
    }
}

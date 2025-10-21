using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FluentResults;
using ToDoApp.BLL.DTOs.ToDoTask;
using ToDoApp.BLL.Validators;

namespace ToDoApp.XunitTests.BLL.Validator
{
    public class BaseTodoTaskValidatorTests
    {
        private readonly BaseTodoTaskValidator _baseTodoTaskValidator;

        public BaseTodoTaskValidatorTests()
        {
            _baseTodoTaskValidator = new BaseTodoTaskValidator();
        }

        [Fact]
        public async Task ShouldPass_WhenDataIsValid()
        {
            var dto = CreateValidDto();

            var result = await _baseTodoTaskValidator.ValidateAsync(dto);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task ValidationFail_WhenTitleIsEmpty()
        {
            var dto = CreateValidDto();
            dto.Title = string.Empty;

            var result = await _baseTodoTaskValidator.ValidateAsync(dto);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task VaditadionFail_WhenTitleTooLong()
        {
            var dto = CreateValidDto();

            dto.Title = new string('b', BaseTodoTaskValidator.MaxTitleLenght + 3);

            var result = await _baseTodoTaskValidator.ValidateAsync(dto);

            result.IsValid.Should().BeFalse();

        }

        [Fact]
        public async Task ValidatiorFail_WhenTitleTooShort()
        {
            var dto = CreateValidDto();

            dto.Title = "a";

            var result = await _baseTodoTaskValidator.ValidateAsync(dto);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task ValidatorFail_WhenDescriptionTooLong()
        {
            var dto = CreateValidDto();

            dto.Description = new string('b', BaseTodoTaskValidator.MaxDescriptionLenght + 3);

            var result = await _baseTodoTaskValidator.ValidateAsync(dto);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task ValidatorFail_WhenDescriptionTooShort()
        {
            var dto = CreateValidDto();
            dto.Description = new string('b', BaseTodoTaskValidator.MinDescriptionLenght - 3);

            var result = await _baseTodoTaskValidator.ValidateAsync(dto);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task ValidatorFail_WhenDueDateEmpty()
        {
            var dto = CreateValidDto();

            dto.DueDate = default;

            var result = await _baseTodoTaskValidator.ValidateAsync(dto);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task ValidatorFail_WhenDueDateInThePast()
        {
            var dto = CreateValidDto();

            dto.DueDate = DateTime.UtcNow.AddDays(-1);

            var result = await _baseTodoTaskValidator.ValidateAsync(dto);

            result.IsValid.Should().BeFalse();
        }

        private CreateToDoTaskDto CreateValidDto()
        {
            return new CreateToDoTaskDto
            {
                Title = "Valid Task",
                Description = "This is a valid description",
                DueDate = DateTime.Now.AddDays(1)
            };
        }
    }
}

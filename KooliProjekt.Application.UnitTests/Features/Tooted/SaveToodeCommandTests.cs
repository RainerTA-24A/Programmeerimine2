using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using KooliProjekt.Application.Features.Tooted;
using KooliProjekt.Application.UnitTests;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace KooliProjekt.UnitTests.Features.Tooted
{
    public class SaveToodeCommandTests : ServiceTestBase
    {
        [Fact]
        public async Task Handle_should_add_new_toode()
        {
            // Arrange
            var command = new SaveToodeCommand { Name = "Test Toode", Price = 10.5m };
            var handler = new SaveToodeCommandHandler(DbContext);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.Equal(1, await DbContext.Tooted.CountAsync());
        }

        [Fact]
        public async Task Handle_should_return_error_when_product_not_found()
        {
            // Arrange
            var command = new SaveToodeCommand { Id = 9999, Name = "Missing" };
            var handler = new SaveToodeCommandHandler(DbContext);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.HasErrors);
        }

        // --- MINU LISATUD: Validaatori testid coverage jaoks ---
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Validator_should_fail_when_Name_is_empty(string name)
        {
            var validator = new SaveToodeCommandValidator();
            var command = new SaveToodeCommand { Name = name, Price = 10 };

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Validator_should_pass_when_data_is_valid()
        {
            var validator = new SaveToodeCommandValidator();
            var command = new SaveToodeCommand { Name = "Korrektne Toode", Price = 5.5m };

            var result = validator.Validate(command);

            Assert.True(result.IsValid);
        }
    }
}
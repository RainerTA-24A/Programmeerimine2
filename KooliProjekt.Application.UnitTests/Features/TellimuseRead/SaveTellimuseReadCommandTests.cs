using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Features.TellimuseRead;
using KooliProjekt.Application.UnitTests;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace KooliProjekt.UnitTests.Features.TellimuseRead
{
    public class SaveTellimuseReadCommandTests : ServiceTestBase
    {
        [Fact]
        public async Task Handle_should_add_new_tellimuse_rida()
        {
            // Arrange
            var command = new SaveTellimuseReadCommand
            {
                Quantity = 5,
                UnitPrice = 10,
                TellimusId = 1,
                ToodeId = 1
            };
            var handler = new SaveTellimuseReadCommandHandler(DbContext);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.Equal(1, await DbContext.TellimusedRida.CountAsync());
        }

        [Fact]
        public async Task Handle_should_return_error_when_id_is_negative()
        {
            // Arrange
            var command = new SaveTellimuseReadCommand { Id = -1 };
            var handler = new SaveTellimuseReadCommandHandler(DbContext);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.HasErrors);
        }
    }
}
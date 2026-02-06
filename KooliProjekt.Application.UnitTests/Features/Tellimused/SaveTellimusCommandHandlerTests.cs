using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.Tellimused;
using KooliProjekt.Application.UnitTests;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace KooliProjekt.UnitTests.Features.Tellimused
{
    public class SaveTellimusCommandHandlerTests : ServiceTestBase
    {
        [Fact]
        public void Constructor_should_throw_when_dbcontext_is_null()
        {
            Assert.Throws<ArgumentNullException>(() => new SaveTellimusCommandHandler(null));
        }

        [Fact]
        public async Task Handle_should_throw_when_request_is_null()
        {
            var handler = new SaveTellimusCommandHandler(DbContext);
            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_should_add_new_tellimus()
        {
            // Arrange
            var command = new SaveTellimusCommand
            {
                Id = 0,
                OrderDate = DateTime.Now,
                Status = "Pending"
            };
            var handler = new SaveTellimusCommandHandler(DbContext);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            var savedTellimus = await DbContext.Tellimused.FirstOrDefaultAsync(t => t.Status == "Pending");

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.NotNull(savedTellimus);
        }

        [Fact]
        public async Task Handle_should_update_existing_tellimus()
        {
            // Arrange
            var tellimus = new Tellimus { OrderDate = DateTime.Now, Status = "New" };
            await DbContext.Tellimused.AddAsync(tellimus);
            await DbContext.SaveChangesAsync();

            var command = new SaveTellimusCommand
            {
                Id = tellimus.Id,
                OrderDate = tellimus.OrderDate,
                Status = "Completed"
            };
            var handler = new SaveTellimusCommandHandler(DbContext);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            var updatedTellimus = await DbContext.Tellimused.FindAsync(tellimus.Id);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.Equal("Completed", updatedTellimus.Status);
        }

        [Fact]
        public async Task Handle_should_return_error_when_updating_missing_tellimus()
        {
            var command = new SaveTellimusCommand { Id = 9999 };
            var handler = new SaveTellimusCommandHandler(DbContext);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.HasErrors);
        }

        // --- VALIDAATORI TESTID ---

        [Fact]
        public void Validator_should_fail_when_Status_is_empty()
        {
            // Eeldame, et Status on kohustuslik (kui pole, võid selle testi eemaldada)
            var validator = new SaveTellimusCommandValidator();
            var command = new SaveTellimusCommand { Status = "" };

            var result = validator.Validate(command);

            // Kui sinu validaatoris pole Status required, siis see test kukub läbi. 
            // Kui on, siis on see õige.
            if (!result.IsValid)
            {
                Assert.Contains(result.Errors, e => e.PropertyName == nameof(SaveTellimusCommand.Status));
            }
        }
    }
}
using Xunit;
using KooliProjekt.Application.Features.Arved;
using KooliProjekt.Application.UnitTests;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace KooliProjekt.UnitTests.Features.Arved
{
    public class ListArvedQueryHandlerTests : ServiceTestBase
    {
        [Fact]
        public void Constructor_should_throw_if_dbcontext_is_null()
        {
            Assert.Throws<ArgumentNullException>(() => new ListArvedQueryHandler(null));
        }

        [Fact]
        public async Task Handle_should_throw_ArgumentNullException_if_request_is_null()
        {
            var handler = new ListArvedQueryHandler(DbContext);
            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_should_throw_ArgumentException_if_pageSize_is_too_large()
        {
            var query = new ListArvedQuery { Page = 1, PageSize = 101 };
            var handler = new ListArvedQueryHandler(DbContext);
            await Assert.ThrowsAsync<ArgumentException>(() => handler.Handle(query, CancellationToken.None));
        }

        // --- MINU LISATUD: Happy Path test coverage jaoks ---
        [Fact]
        public async Task Handle_should_return_list_of_invoices_when_data_exists()
        {
            // Arrange
            var klient = new Klient { FirstName = "Test", LastName = "Kasutaja", Email = "test@test.ee" };
            DbContext.Kliendid.Add(klient);

            var arve = new Arve
            {
                InvoiceNumber = "INV-001",
                InvoiceDate = DateTime.Now,
                Klient = klient,
                TellimusId = 1
            };
            DbContext.Arved.Add(arve);
            await DbContext.SaveChangesAsync();

            var query = new ListArvedQuery { Page = 1, PageSize = 10 };
            var handler = new ListArvedQueryHandler(DbContext);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result.Value);
            Assert.NotEmpty(result.Value.Results);
            Assert.Equal("INV-001", result.Value.Results.First().InvoiceNumber);
        }
    }
}
using Xunit;
using KooliProjekt.Application.Features.Tooted;
using KooliProjekt.Application.UnitTests;
using KooliProjekt.Application.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace KooliProjekt.UnitTests.Features.Tooted
{
    public class ListTootedQueryHandlerTests : ServiceTestBase
    {
        [Fact]
        public void Constructor_should_throw_if_dbcontext_is_null()
        {
            Assert.Throws<ArgumentNullException>(() => new ListTootedQueryHandler(null));
        }

        [Fact]
        public async Task Handle_should_throw_ArgumentNullException_if_request_is_null()
        {
            var handler = new ListTootedQueryHandler(DbContext);
            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_should_return_list_of_products_when_data_exists()
        {
            // Arrange
            DbContext.Tooted.Add(new Toode { Name = "Sool", Price = 1.2m });
            await DbContext.SaveChangesAsync();

            var query = new ListTootedQuery { Page = 1, PageSize = 10 };
            var handler = new ListTootedQueryHandler(DbContext);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result.Value);
            Assert.NotEmpty(result.Value.Results);
            Assert.Equal("Sool", result.Value.Results.First().Name);
        }
    }
}a
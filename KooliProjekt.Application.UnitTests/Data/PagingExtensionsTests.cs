using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.UnitTests;
using Xunit;

namespace KooliProjekt.UnitTests.Data
{
    public class PagingExtensionsTests : ServiceTestBase
    {
        [Fact]
        public async Task GetPagedAsync_should_return_correct_paged_result()
        {
            // Arrange - lisame 15 toodet
            for (int i = 1; i <= 15; i++)
            {
                DbContext.Tooted.Add(new Toode { Name = $"Toode {i}", Price = 10 });
            }
            await DbContext.SaveChangesAsync();

            var query = DbContext.Tooted.AsQueryable();

            // Act - küsime esimest lehte, kus on 10 asja
            var result = await query.GetPagedAsync(1, 10);

            // Assert
            Assert.Equal(1, result.CurrentPage);
            Assert.Equal(10, result.PageSize);
            Assert.Equal(15, result.RowCount);
            Assert.Equal(2, result.PageCount); // 15 asja / 10 per leht = 2 lehte
            Assert.Equal(10, result.Results.Count);
        }
    }
}
using System.Linq;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.UnitTests;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace KooliProjekt.UnitTests.Data
{
    public class SeedDataTests : ServiceTestBase
    {
        [Fact]
        public void Generate_should_populate_database_with_initial_data()
        {
            // Arrange
            // Konstruktor ootab DbContexti, mille saame ServiceTestBase-st
            var seeder = new SeedData(DbContext);

            // Act
            // Meetod ise ei taha sulgude vahele midagi
            seeder.Generate();

            // Assert
            Assert.True(DbContext.Kliendid.Any(), "Kliendid peaksid olema genereeritud");
            Assert.True(DbContext.Tooted.Any(), "Tooted peaksid olema genereeritud");
            Assert.True(DbContext.Tellimused.Any(), "Tellimused peaksid olema genereeritud");
            Assert.True(DbContext.Arved.Any(), "Arved peaksid olema genereeritud");
        }
    }
}
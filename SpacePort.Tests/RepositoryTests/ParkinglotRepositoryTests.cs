using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using SpacePort.Services.Repositories;
using SpacePort.Models;
using System.Collections.Generic;
using Xunit;

namespace SpacePort.Tests.RepositoryTests
{
    public class ParkinglotRepositoryTests
    {
        [Fact]
        public async void GetAllParkinglots_ReturnsObjects_ExpectedTrue()
        {
            // Arrange
            var mockContext = new Mock<DataContext>();
            mockContext.Setup(p => p.Parkinglots).ReturnsDbSet(new List<Parkinglot> 
            {
                new Parkinglot { ParkinglotId = 1, Name = "Coruscant" },
                new Parkinglot { ParkinglotId = 2, Name = "Naboo" }
            });

            var logger = Mock.Of<ILogger<ParkinglotRepository>>();
            var parkinglotRepository = new ParkinglotRepository(mockContext.Object, logger);

            // Act
            var result = await parkinglotRepository.GetAll();

            // Assert
            Assert.True(result.Length == 2);
        }
    }
}

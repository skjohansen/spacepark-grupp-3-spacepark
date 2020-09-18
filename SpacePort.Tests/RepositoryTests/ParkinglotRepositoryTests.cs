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

        [Theory]
        [InlineData(1, "LotA")]
        [InlineData(2, "LotB")]
        public async void GetParkinglotByIdTest_CheckIfReturnedObject_ContainsCorrectName(int inlineInt, string expected)
        {
            //Arrange
            IList<Parkinglot> parkinglots = GenerateParkinglots();
            var mockContext = new Mock<DataContext>();
            mockContext.Setup(p => p.Parkinglots).ReturnsDbSet(parkinglots);

            var logger = Mock.Of<ILogger<ParkinglotRepository>>();
            var parkinglotRepository = new ParkinglotRepository(mockContext.Object, logger);

            //Act
            var theParkinglot = await parkinglotRepository.GetParkinglotById(inlineInt);

            //Assert
            Assert.Equal(expected, theParkinglot.Name);
        }

        private static IList<Parkinglot> GenerateParkinglots()
        {
            return new List<Parkinglot>
            {
                new Parkinglot
                {
                    ParkinglotId = 1,
                    Name = "LotA"

                },
                new Parkinglot
                {
                    ParkinglotId = 2,
                    Name = "LotB"
                }
            };
        }

    }
}

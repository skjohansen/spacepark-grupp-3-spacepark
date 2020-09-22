using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using SpacePort.Models;
using SpacePort.Services.Repositories;
using Xunit;

namespace SpacePort.Tests.RepositoryTests
{
    public class ParkingspotRepositoryTests
    {
        [Fact]
        public async void GetAll_ifAnyExist_ReturnTrue()
        {
            //Arrange
            var mockContext = new Mock<DataContext>();
            mockContext.Setup(p => p.Parkingspots).ReturnsDbSet(GetParkingspots());

            var logger = Mock.Of<ILogger<ParkingspotRepository>>();
            var parkingspotRepository = new ParkingspotRepository(mockContext.Object, logger);

            //Act
            var result = await parkingspotRepository.GetAll();

            // Assert
            Assert.True(result.Length == 2);
        }

        [Fact]
        public async void GetParkingspotById_ifExist_ExpectedNotNull()
        {
            //Arrange
            var mockContext = new Mock<DataContext>();
            mockContext.Setup(p => p.Parkingspots).ReturnsDbSet(GetParkingspots());

            var logger = Mock.Of<ILogger<ParkingspotRepository>>();
            var parkingspotRepository = new ParkingspotRepository(mockContext.Object, logger);

            //Act
            var result = await parkingspotRepository.GetparkingspotById(1);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetParkingspotById_ifDoesNotExist_ExpectedIsNull()
        {
            //Arrange
            var mockContext = new Mock<DataContext>();
            mockContext.Setup(p => p.Parkingspots).ReturnsDbSet(GetParkingspots());

            var logger = Mock.Of<ILogger<ParkingspotRepository>>();
            var parkingspotRepository = new ParkingspotRepository(mockContext.Object, logger);

            //Act
            var result = await parkingspotRepository.GetparkingspotById(3);

            //Assert
            Assert.Null(result);
        }

        public List<Parkingspot> GetParkingspots()
        {
            return new List<Parkingspot>
            {
                new Parkingspot
                {
                    ParkingspotId = 1, 
                    Occupied = true, 
                    Size = 2
                },
                new Parkingspot 
                { 
                    ParkingspotId = 2, 
                    Occupied = false, 
                    Size = 1
                }
            };
        }
    }
}

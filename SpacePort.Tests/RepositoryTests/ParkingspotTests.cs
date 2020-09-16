using System;
using System.Collections.Generic;
using System.Text;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using SpacePort.Models;
using SpacePort.Services.Repositories;
using Xunit;

namespace SpacePort.Tests.RepositoryTests
{
    public class ParkingspotTests
    {
        [Fact]
        public async void GettAllParkingspots()
        {
            //Arrange
            var mockContext = new Mock<DataContext>();
            mockContext.Setup(p => p.Parkingspots).ReturnsDbSet(new List<Parkingspot>
            {
                new Parkingspot {ParkingspotId = 1, Occupied = true, Size = 2},
                new Parkingspot { ParkingspotId = 2, Occupied = false, Size = 1}
            });

            var logger = Mock.Of<ILogger<ParkingspotRepository>>();
            var parkingspotRepository = new ParkingspotRepository(mockContext.Object, logger);

            //Act
            var result = await parkingspotRepository.GetAll();

            // Assert
            Assert.True(result.Length == 1);
        }
    }
}

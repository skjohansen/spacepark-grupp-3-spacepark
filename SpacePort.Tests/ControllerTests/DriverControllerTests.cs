using SpacePort.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using Moq.EntityFrameworkCore;
using Castle.Core.Logging;
using SpacePort.Services.Repositories;
using Microsoft.Extensions.Logging;

namespace SpacePort.Tests.ControllerTests
{
    public class DriverControllerTests
    {
        [Theory]
        [InlineData(1, "Luke")]
        [InlineData(2, "Yoda")]
        public async void GetDriverByIdTest_CheckIfReturnedObject_ContainsCorrectName(int inlineInt, string expected)
        {
            //Arrange
            IList<Driver> drivers = GenerateDrivers();
            var mockContext = new Mock<DataContext>();
            mockContext.Setup(d => d.Drivers).ReturnsDbSet(drivers);

            var logger = Mock.Of<ILogger<DriverRepository>>();
            var driverRepository = new DriverRepository(mockContext.Object, logger);

            //Act
            var theDriver = await driverRepository.GetDriverById(inlineInt);

            //Assert
            Assert.Equal(expected, theDriver.Name);
        }
        private static IList<Driver> GenerateDrivers()
        {
            return new List<Driver>
            {
                new Driver
                {
                    DriverId = 1,
                    Name = "Luke"
                },
                new Driver
                {
                    DriverId = 2,
                    Name = "Yoda"
                }
            };
        }
    }
}

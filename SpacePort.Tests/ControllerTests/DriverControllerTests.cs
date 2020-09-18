using Moq;
using SpacePort.Models;
using System.Collections.Generic;
using Moq.EntityFrameworkCore;
using Xunit;
using SpacePort.Controllers;
using Microsoft.Extensions.Logging;
using SpacePort.Services.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace SpacePort.Tests.ControllerTests
{
    public class DriverControllerTests
    {
        [Fact]
        public async void GetAll_ifAnyExist_ReturnTrue()
        {
            //Arrange
            var mockContext = new Mock<DataContext>();
            mockContext.Setup(x => x.Drivers).ReturnsDbSet(GetDriver());

            var logger = Mock.Of<ILogger<DriverRepository>>();
            var driverRepo = new DriverRepository(mockContext.Object, logger);

            var driverController = new DriverController(driverRepo);

            //Act
            var result = await driverController.GetAll();
            var contentResult = result.Result as OkObjectResult;
            var resultDriver = contentResult.Value as Driver[];

            //Assert
            Assert.True(resultDriver.Length > 0);
        }

        [Fact]
        public async void GetDriverById_ifExist_ExpectedNotNull()
        {
            //Arrange
            var mockContext = new Mock<DataContext>();
            mockContext.Setup(x => x.Drivers).ReturnsDbSet(GetDriver());

            var logger = Mock.Of<ILogger<DriverRepository>>();
            var driverRepo = new DriverRepository(mockContext.Object, logger);

            var driverController = new DriverController(driverRepo);

            //Act
            var result = await driverController.GetDriverById(1);

            //Assert
            Assert.NotNull(result.Value);
        }

        [Fact]
        public async void GetDriverById_ifDoesNotExist_ExpectedIsNull()
        {
            //Arrange
            var mockContext = new Mock<DataContext>();
            mockContext.Setup(x => x.Drivers).ReturnsDbSet(GetDriver());

            var logger = Mock.Of<ILogger<DriverRepository>>();
            var driverRepo = new DriverRepository(mockContext.Object, logger);

            var driverController = new DriverController(driverRepo);

            //Act
            var result = await driverController.GetDriverById(2);

            //Assert
            Assert.Null(result.Value);
        }

        public List<Driver> GetDriver()
        {
            return new List<Driver>
            {
                new Driver
                {
                    DriverId=1,
                    Name="Pierre"
                }
            };
        }
    }
}

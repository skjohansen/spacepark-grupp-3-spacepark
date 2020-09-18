using Moq;
using SpacePort.Models;
using System.Collections.Generic;
using Moq.EntityFrameworkCore;
using Xunit;
using SpacePort.Services.Repositories;
using Microsoft.Extensions.Logging;

namespace SpacePort.Tests.RepositoryTests
{
    public class DriverRepositoryTests
    {
        [Fact]
        public async void GetAll_ifAnyExist_ReturnTrue()
        {
            //Arrange
            var mockContext = new Mock<DataContext>();
            mockContext.Setup(x => x.Drivers).ReturnsDbSet(GetDriver());

            var logger = Mock.Of<ILogger<DriverRepository>>();
            var driverRepo = new DriverRepository(mockContext.Object, logger);

            //Act
            var result = await driverRepo.GetAll();

            //Assert
            Assert.True(result.Length > 0);

        }

        [Fact]
        public async void GetDriverById_ifExist_AssertNotNull()
        {
            //Arrange
            var mockContext = new Mock<DataContext>();
            mockContext.Setup(x => x.Drivers).ReturnsDbSet(GetDriver());

            var logger = Mock.Of<ILogger<DriverRepository>>();
            var driverRepo = new DriverRepository(mockContext.Object, logger);

            //Act
            var result = await driverRepo.GetDriverById(1);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]

        public async void GetDriverById_ifDoesNotExist_AssertIsNull()
        {
            //Arrange
            var mockContext = new Mock<DataContext>();
            mockContext.Setup(x => x.Drivers).ReturnsDbSet(GetDriver());

            var logger = Mock.Of<ILogger<DriverRepository>>();
            var driverRepo = new DriverRepository(mockContext.Object, logger);

            //Act
            var result = await driverRepo.GetDriverById(2);

            //Assert
            Assert.Null(result);
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

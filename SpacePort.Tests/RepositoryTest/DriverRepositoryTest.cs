using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Moq;
using SpacePort.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Moq.EntityFrameworkCore;
using Xunit;
using Castle.Core.Logging;
using SpacePort.Services.Repositories;
using Microsoft.Extensions.Logging;

namespace SpacePort.Tests.RepositoryTest
{
    public class DriverRepositoryTest
    {
        [Fact]
        public async void GetAll_ifAnyExist_ReturnTrue()
        {
            //Arrange
            var context = new Mock<DataContext>();
            context.Setup(x => x.Drivers).ReturnsDbSet(GetDriver());

            var logger = Mock.Of<ILogger<DriverRepository>>();
            var driverRepo = new DriverRepository(context.Object, logger);

            //Fact
            var result = await driverRepo.GetAll();

            //Assert
            Assert.True(result.Length>0);

        }
        public List<Driver> GetDriver()
        {
            return new List<Driver>
            {
                new Driver
                {
                    DriverId=1,
                    Name="Pierre", 
                    Receipts= null
                }
            };

        }
    }
}

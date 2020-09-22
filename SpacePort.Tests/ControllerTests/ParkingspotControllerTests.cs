using Moq;
using SpacePort.Models;
using System.Collections.Generic;
using Moq.EntityFrameworkCore;
using Xunit;
using SpacePort.Controllers;
using Microsoft.Extensions.Logging;
using SpacePort.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using SpacePort.Services.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using static SpacePort.Controllers.ParkingspotController;

namespace SpacePort.Tests.ControllerTests
{
    public class ParkingspotControllerTests
    {
        [Fact]
        public async void GetAll_IfAnyExist_ReturnTrue()
        {
            //Arrange
            var mockContext = new Mock<DataContext>();
            mockContext.Setup(p => p.Parkingspots).ReturnsDbSet(GetParkingspot());

            var logger = Mock.Of<ILogger<ParkingspotRepository>>();
            var parkingspotRepo = new ParkingspotRepository(mockContext.Object, logger);

            var parkingspotController = new ParkingspotController(parkingspotRepo);

            //Act
            var result = await parkingspotController.GetAll();
            var contentResult = result.Result as OkObjectResult;
            var resultParkingspot = contentResult.Value as Parkingspot[];

            //Assert
            Assert.True(resultParkingspot.Length > 0);
        }

        [Fact]
        public async void GetParkingspotById_IfExist_returnTrue()
        {
            //Arrange
            var mockContext = new Mock<DataContext>();
            mockContext.Setup(x => x.Parkingspots).ReturnsDbSet(GetParkingspot());

            var logger = Mock.Of<ILogger<ParkingspotRepository>>();
            var parkingspotRepo = new ParkingspotRepository(mockContext.Object, logger);

            var parkingspotController = new ParkingspotController(parkingspotRepo);

            //Act
            var result = await parkingspotController.GetParkingspotById(1);
            var contentResult = result.Result as OkObjectResult;
            var resultParkingspot = contentResult.Value as Parkingspot;

            //Assert
            Assert.NotNull(resultParkingspot);
        }
        [Fact]
        public async void UpdateParkingspotById_IfIdExist_Expect200StatusCode()
        {
            //Arange
            var parkingspotRepo = new Mock<IParkingspotRepository>();
            parkingspotRepo.Setup(x => x.GetparkingspotById(It.IsAny<int>()))
                .Returns(Task.FromResult(new Parkingspot()));
            parkingspotRepo.Setup(x => x.Save()).Returns(Task.FromResult(true));

            var parkingspotController = new ParkingspotController(parkingspotRepo.Object);
            var parkingspotRequest = new PutParkingspotRequest
            {
                ParkingspotId = 1,
                Occupied = true
            };
            //Act
            var result = await parkingspotController.UpdateParkingspot(parkingspotRequest);
            var contentResult = result.Result as OkObjectResult;

            //Assert
            Assert.Equal(200, contentResult.StatusCode);

            
        }
        public List<Parkingspot> GetParkingspot()
        {
            return new List<Parkingspot>
            {

                new Parkingspot
                {
                    ParkingspotId = 1,
                    Size = 3,
                    Occupied = false
                }
            };
        }
    }
}

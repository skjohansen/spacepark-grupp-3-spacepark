using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using SpacePort.Controllers;
using SpacePort.Models;
using SpacePort.Services.Interfaces;
using SpacePort.Services.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using static SpacePort.Controllers.ParkinglotController;

namespace SpacePort.Tests.ControllerTests
{
    public class ParkinglotControllerTests
    {
        [Fact]
        public async void GetAll_IfAnyExist_ExpectedTrue()
        {
            //Arrange
            var mockContext = new Mock<DataContext>();
            mockContext.Setup(x => x.Parkinglots).ReturnsDbSet(GetParkinglot());

            var logger = Mock.Of<ILogger<ParkinglotRepository>>();
            var parkinglotRepo = new ParkinglotRepository(mockContext.Object, logger);

            var parkinglotController = new ParkinglotController(parkinglotRepo);

            //Act
            var result = await parkinglotController.GetAll();
            var contentResult = result.Result as OkObjectResult;
            var resultParkinglot = contentResult.Value as Parkinglot[];

            //Assert
            Assert.True(resultParkinglot.Length > 0);
        }

        [Fact]
        public async void GetParkinglotById_IfExist_ExpectedNotNull()
        {
            //Arrange
            var mockContext = new Mock<DataContext>();
            mockContext.Setup(x => x.Parkinglots).ReturnsDbSet(GetParkinglot());

            var logger = Mock.Of<ILogger<ParkinglotRepository>>();
            var parkinglotRepo = new ParkinglotRepository(mockContext.Object, logger);

            var parkinglotController = new ParkinglotController(parkinglotRepo);

            //Act
            var result = await parkinglotController.GetParkinglotById(1);
            var contentResult = result.Result as OkObjectResult;
            var resultParkinglot = contentResult.Value as Parkinglot;

            //Assert
            Assert.NotNull(resultParkinglot);
        }

        [Fact]
        public async void GetAvailableParkingspot_IfGetAvailableParkingspot_Expected200StatusCode()
        {
            //Arrange
            var parkinglotRepo = new Mock<IParkinglotRepository>();
            parkinglotRepo.Setup(x => x.GetParkinglotById(1)).Returns(Task.FromResult(new Parkinglot { ParkinglotId = 1, Parkingspot = GetParkingSpots()}));
            parkinglotRepo.Setup(x => x.Save()).Returns(Task.FromResult(true));

            var parkinglotController = new ParkinglotController(parkinglotRepo.Object);

            //Act
            var okResult = await parkinglotController.GetAvailableParkingspot(new PostParkingspotRequest 
            { 
                ParkinglotId = 1, 
                Shipsize = 1 
            });
            var contentResult = okResult.Result as OkObjectResult;

            //Assert
            Assert.Equal(200, contentResult.StatusCode);
        }

        public List<Parkinglot> GetParkinglot()
        {
            return new List<Parkinglot>
            {
                new Parkinglot
                {
                    ParkinglotId = 1,
                    Name = "Parkinglot 1"
                }
            };
        }

        public List<Parkingspot> GetParkingSpots()
        {
            return new List<Parkingspot>
            {
                new Parkingspot
                {
                    ParkingspotId = 1,
                    Occupied = false,
                    Size = 1
                }
            };
        }
    }
}

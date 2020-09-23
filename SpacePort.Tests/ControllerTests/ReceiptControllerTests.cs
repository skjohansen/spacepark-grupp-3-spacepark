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
using static SpacePort.Controllers.ReceiptController;

namespace SpacePort.Tests.ControllerTests
{
    public class ReceiptControllerTests
    {
        [Fact]
        public async void GetAll_IfAnyExist_ExpectedTrue()
        {
            //Arrange
            var mockContext = new Mock<DataContext>();
            mockContext.Setup(x => x.Receipts).ReturnsDbSet(GetReceipts());

            var receiptLogger = Mock.Of<ILogger<ReceiptRepository>>();
            var receiptRepo = new ReceiptRepository(mockContext.Object, receiptLogger);
            var driverLogger = Mock.Of<ILogger<DriverRepository>>();
            var driverRepo = new DriverRepository(mockContext.Object, driverLogger);
            var parkingspotLogger = Mock.Of<ILogger<ParkingspotRepository>>();
            var parkingspotRepo = new ParkingspotRepository(mockContext.Object, parkingspotLogger);

            var receiptController = new ReceiptController(receiptRepo, driverRepo, parkingspotRepo);

            //Act
            var result = await receiptController.GetAll();
            var contentResult = result.Result as OkObjectResult;
            var resultReceipt = contentResult.Value as Receipt[];

            //Assert
            Assert.True(resultReceipt.Length > 0);
        }

        [Fact]
        public async void GetReceiptById_IfExist_ExpectedNotNull()
        {
            //Arrange
            var mockContext = new Mock<DataContext>();
            mockContext.Setup(x => x.Receipts).ReturnsDbSet(GetReceipts());

            var receiptLogger = Mock.Of<ILogger<ReceiptRepository>>();
            var receiptRepo = new ReceiptRepository(mockContext.Object, receiptLogger);
            var driverLogger = Mock.Of<ILogger<DriverRepository>>();
            var driverRepo = new DriverRepository(mockContext.Object, driverLogger);
            var parkingspotLogger = Mock.Of<ILogger<ParkingspotRepository>>();
            var parkingspotRepo = new ParkingspotRepository(mockContext.Object, parkingspotLogger);

            var receiptController = new ReceiptController(receiptRepo, driverRepo, parkingspotRepo);

            //Act
            var result = await receiptController.GetReceiptById(1);
            var contentResult = result.Result as OkObjectResult;
            var resultReceipt = contentResult.Value as Receipt;

            //Assert
            Assert.NotNull(resultReceipt);
        }

        [Fact]
        public async void CreateReceipt_IfCreateReceipt_Expected201StatusCode()
        {
            //Arrange
            var receiptRepo = new Mock<IReceiptRepository>();
            receiptRepo.Setup(x => x.Save()).Returns(Task.FromResult(true));
            var driverRepo = new Mock<IDriverRepository>();
            var parkingspotRepo = new Mock<IParkingspotRepository>();

            var receiptController = new ReceiptController(receiptRepo.Object, driverRepo.Object, parkingspotRepo.Object);

            //Act
            var createdResult = await receiptController.CreateReceipt(new PostReceipt
            {
                DriverId = new Driver { DriverId = 1 }.DriverId,
                ParkingspotId = new Parkingspot { ParkingspotId = 1 }.ParkingspotId
            });
            var contentResult = createdResult.Result as CreatedResult;

            //Assert
            Assert.Equal(201, contentResult.StatusCode);
        }

        public List<Receipt> GetReceipts()
        {
            return new List<Receipt>
            {
                new Receipt 
                { 
                    ReceiptId = 1 
                },
                new Receipt 
                { 
                    ReceiptId = 2 
                },
                new Receipt 
                { 
                    ReceiptId = 3 
                }
            };
        }
    }
}

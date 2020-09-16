using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using PakingAPI.Services;
using SpacePort.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace SpacePort.Tests.RepositoryTests
{
    public class ReceiptRepositoryTests
    {
        [Fact]
        public async void GetAllReceipts_ReturnsObjects_ExpectedTrue()
        {
            // Arrange
            var mockContext = new Mock<DataContext>();
            mockContext.Setup(p => p.Receipts).ReturnsDbSet(new List<Receipt> 
            {
                new Receipt { ReceiptId = 1 },
                new Receipt { ReceiptId = 2 },
                new Receipt { ReceiptId = 3 }
            });

            var logger = Mock.Of<ILogger<ReceiptRepository>>();
            var receiptRepository = new ReceiptRepository(mockContext.Object, logger);

            // Act
            var result = await receiptRepository.GetAll();

            // Assert
            Assert.True(result.Length == 3);
        }

        [Fact]
        public async void GetReceiptById_ReturnsSpecificObject_ExpectedCorrectId()
        {
            // Arrange
            var mockContext = new Mock<DataContext>();
            mockContext.Setup(p => p.Receipts).ReturnsDbSet(new List<Receipt>
            {
                new Receipt { ReceiptId = 1 },
                new Receipt { ReceiptId = 2 },
                new Receipt { ReceiptId = 3 }
            });

            var logger = Mock.Of<ILogger<ReceiptRepository>>();
            var receiptRepository = new ReceiptRepository(mockContext.Object, logger);

            // Act
            var expectedId = 2;
            var result = await receiptRepository.GetReceiptById(expectedId);

            // Assert
            Assert.Equal(expectedId, result.ReceiptId);
        }
    }
}

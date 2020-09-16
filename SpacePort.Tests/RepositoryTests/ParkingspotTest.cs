using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using SpacePort.Models;
using Xunit;

namespace SpacePort.Tests.RepositoryTests
{
    public class ParkingspotTest
    {
        [Fact]
        public async void GettAllParkingspots()
        {
            //Arrange
            var mockContext = new Mock<DataContext>();
            mockContext.Setup(p => p.Parkingspots).ReturnsDbset(new List<Parkingspot>
            {
                new Parkingspot { ParkingspotId = 1, Name = ""}
            })
        }
    }
}

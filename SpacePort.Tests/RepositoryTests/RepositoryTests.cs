using Microsoft.Extensions.Logging;
using Moq;
using SpacePort.Models;
using SpacePort.Services.Repositories;
using Xunit;

namespace SpacePort.Tests.RepositoryTests
{
    public class RepositoryTests
    {
        [Fact]
        public async void Add_ifObjectSaved_ExpectedTrue()
        {
            //Arrange
            var context = new Mock<DataContext>();
            var logger = Mock.Of<ILogger<Repository>>();

            var repo = new Repository(context.Object, logger);
            repo.Add(GetDriver());

            //ACT
            var result =await repo.Save();

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void Update_ifEntityUpdated_ExpectedTrue()
        {
            //Arrange
            var context = new Mock<DataContext>();
            var logger = Mock.Of<ILogger<Repository>>();

            var repo = new Repository(context.Object,logger);
            repo.Update(GetDriver());

            //Act
            var result = await repo.Save();

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void Delete_ifEntityDeleted_ExpectedTrue()
        {
            //Arrange
            var context = new Mock<DataContext>();
            var logger = Mock.Of<ILogger<Repository>>();

            var repo = new Repository(context.Object, logger);
            repo.Delete(GetDriver());

            //Act
            var result = await repo.Save();

            //Assert
            Assert.True(result);
        }
        public Driver GetDriver()
        {
            return new Driver
                {
                    DriverId = 1,
                    Name = "Pierre"
                };
        }
    }
}

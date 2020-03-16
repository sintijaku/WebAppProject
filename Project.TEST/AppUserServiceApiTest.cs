using Project.MVC.Services;
using Xunit;
using FluentAssertions;
using Project.DATA.Context;
using Project.MODEL.Entities;


namespace Project.TEST
{
    public class AppUserServiceApiTest : TestBase
    {
        private AppUserService _service;
        private MyContext dbContext;

        public AppUserServiceApiTest()
        {
            dbContext = GetDbContext();

            dbContext.AppUsers.Add(new AppUser { FirstName = "Mike", LastName = "Smith", ID = 1, IsActive = true });
            dbContext.AppUsers.Add(new AppUser { FirstName = "Dan", LastName = "Doe", ID = 2, IsActive = true });
            dbContext.SaveChanges();

            _service = new AppUserService(dbContext);
        }

        [Fact]
        public void GetAll_ShouldNotBeNull()
        {
            //Arrange
            //Act
            var result = _service.GetAll();
            //Assert
            result.Should().NotBeNull();
            dbContext.Dispose();
        }

        [Fact]
        public void GetById_ShouldNotBeNull()
        {
            //Arrange
            //Act
            var result = _service.GetById(1);
            //Assert
            result.Should().NotBeNull();
            dbContext.Dispose();
        }

        [Fact]
        public void Any_ShouldReturnTrueForExistingID()
        {
            //Arrange
            //Act
            var result = _service.Any(2);
            //Assert
            result.Should().BeTrue();
            dbContext.Dispose();
        }

    }
}


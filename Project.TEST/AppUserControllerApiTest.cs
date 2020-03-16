using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Project.API.Controllers;
using Project.MODEL.Entities;
using Project.MVC.Services;
using Xunit;
using FluentAssertions;
using Project.DATA.Context;

namespace Project.TEST
{
    public class AppUserControllerApiTest : TestBase
    {
        private IAppUserService _service;
        private AppUserController _controller;
        private MyContext dbContext;

        public AppUserControllerApiTest()
        {
            dbContext = GetDbContext();

            dbContext.AppUsers.Add(new AppUser { FirstName = "Mike", LastName = "Smith", ID = 1, IsActive = true });
            dbContext.AppUsers.Add(new AppUser { FirstName = "Dan", LastName = "Doe", ID = 2, IsActive = true });
            dbContext.SaveChanges();

            _service = new AppUserService(dbContext);
            _controller = new AppUserController(_service);
        }

        [Fact]
        public void GetById_Returs()
        {
            var result = _controller.GetUser(1);
            result.Should().NotBeNull();
            dbContext.Dispose();
        }

        [Fact]
        public void GetAllUsers_ReturnNotNull()
        {
            var result = _controller.GettAllUsers();
            result.Should().NotBeNull();
            dbContext.Dispose();
        }

        [Fact]
        public void GetUser_ReturnNotFoundForNotExistingID()
        {
            var result = _controller.GetUser(33);
            Assert.IsType<NotFoundResult>(result.Result);
            dbContext.Dispose();
        }

        [Fact]
        public void GetUser_ReturnOkObjectResultForExistingID()
        {
            var result = _controller.GetUser(1);
            Assert.IsType<OkObjectResult>(result.Result);
            dbContext.Dispose();
        }

        [Fact]
        public void Delete_ReturnsNotFoundForInvalidID()
        {
            var result = _controller.Delete(77);
            result.Should().BeOfType<NotFoundResult>();
            dbContext.Dispose();
        }

        [Fact]
        public void Delete_ReturnsBadRequestForNullId()
        {
            var result = _controller.Delete(0);
            result.Should().BeOfType<BadRequestResult>();
            dbContext.Dispose();
        }


    }
}

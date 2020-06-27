using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using BoardWebApp.Controllers;
using BoardWebApp.Models;
using BoardWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BoardWebApp_Tests
{
    public class LoginValidationTests : IDisposable
    {
        private BoardWebAppContext _dbContext;
        private readonly ITestOutputHelper _output;

        public LoginValidationTests(ITestOutputHelper testOutputHelper)
        {
            _output = testOutputHelper;
            _dbContext = InMemoryDb.InitInMemoryDbContext();
        }
        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();;
        }


        // --------------------- This test verifies that whenever a user fails to login, they're displayed with the same page and an error message is to the view. ---------------------
        [Theory]
        //[InlineData("trudy@board.com", "P@ssword1", true)] // All good --- Pavel: I can't mock the Http request. Give it a go if you want, would be good test to make sure we don't break it.
        [InlineData("", "P@ssword1", false)] // email empty
        [InlineData("trudy@board.com", "", false)] // password empty
        [InlineData("trudy@board.com", "P@ssword", false)] // Wrong password
        [InlineData("trudy@board.c", "P@ssword1", false)] // user doesn't exist
        public void PostLoginDisplaysErrorMessage(string email, string password, bool expectedResult)
        {
            SendLoginModel loginInfo = new SendLoginModel()
            {
                userLoginModel = new UserLoginModel()
                {
                    Email = email,
                    Password = password
                }
            };

            var result = new AccountController(_dbContext).Login(loginInfo);
            var requestResult = Assert.IsType<ViewResult>(result);
            var resultModel = Assert.IsAssignableFrom<SendLoginModel>(requestResult.ViewData.Model);

            //Verify that the error message is populated on the view's Model
            Assert.True(String.IsNullOrEmpty(resultModel.ErrorMessage) != true);
        }
    }
}

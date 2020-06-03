using BoardWebApp.Controllers;
using BoardWebApp.Models;
using BoardWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace BoardWebApp_Tests
{
    public class LoginValidationTests
    {
        private BoardWebAppContext _dbContext;
        private readonly ITestOutputHelper _output;

        public LoginValidationTests(ITestOutputHelper testOutputHelper)
        {
            _output = testOutputHelper;
        }

        [Theory]
        //[InlineData("trudy@board.com", "P@ssword1", true)] // All good
        [InlineData("", "P@ssword1", false)] // email empty
        [InlineData("trudy@board.com", "", false)] // password empty
        [InlineData("trudy@board.com", "P@ssword", false)] // Wrong password
        [InlineData("trudy@board.c", "P@ssword1", false)] // user doesn't exist
        public void PostLoginDisplaysErrorMessage(string email, string password, bool expectedResult)
        {
            _dbContext = RegistrationValidationTests.InitInMemoryDbContext();
            foreach(User u in _dbContext.User)
            {
                _output.WriteLine("User available for test - " + u.Email);
            }

            SendLoginModel loginInfo = new SendLoginModel()
            {
                userLoginModel = new UserLoginModel()
                {
                    Email = email,
                    Password = password
                }
            };

            _output.WriteLine("form data - email: " + loginInfo.userLoginModel.Email);

            var result = new AccountController(_dbContext).Login(loginInfo);

            //if (expectedResult == true)
            //{
            //    _output.WriteLine("Log in success");
            //    Assert.IsType<RedirectToActionResult>(result);
            //}
            //else
            //{
                _output.WriteLine("Log in failed");
                var requestResult = Assert.IsType<ViewResult>(result);
                var resultModel = Assert.IsAssignableFrom<SendLoginModel>(requestResult.ViewData.Model);

                //Verify that the error message is populated on the view's Model
                Assert.True(String.IsNullOrEmpty(resultModel.ErrorMessage) != true);

                _dbContext = null;
            //}
        }
    }
}

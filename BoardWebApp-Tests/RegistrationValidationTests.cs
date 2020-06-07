using BoardWebApp.Controllers;
using BoardWebApp.Models;
using BoardWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace BoardWebApp_Tests
{

    public class RegistrationValidationTests : IDisposable
    {
        private BoardWebAppContext _dbContext;
        private List<string> _validations;
        private readonly ITestOutputHelper _output;

        public RegistrationValidationTests(ITestOutputHelper testOutputHelper)
        {
            _validations = new List<string>();
            _output = testOutputHelper;
            _dbContext = InMemoryDb.InitInMemoryDbContext();
        }

        public void Dispose()
        {
            _validations.Clear();
            _dbContext.Database.EnsureDeleted();
        }


        [Theory]
        [InlineData("Antony.Hopkins@Mail.com", null)] //email is not in DB, no error
        [InlineData("notaracist@murica.com", "Email is already in use by another user!")]
        [InlineData("aaaaaaaa", "Please enter a valid email address!")]
        public void EmailValidations(string email, string expectedValidationMessage)
        {
            _validations.Clear(); // Just in case

            string actualValidationMessage = null;

            UserRegistrationModel.EmailValidationsStatic(email, _validations, _dbContext);
            if (_validations != null && _validations.Count > 0)
            {
                actualValidationMessage = _validations[0];
            }
            Assert.Equal(expectedValidationMessage, actualValidationMessage);

            _validations.Clear();
        }
        
        [Theory]
        [InlineData("al@walk.com", "Alan", "Alan", "P@ssword1", "P@ssword1", 0)] // All good
        [InlineData("al@walk.com", "", "Alan", "P@ssword1", "P@ssword1", 1)] // First Name empty
        [InlineData("al@walk.com", "Alan", "", "P@ssword1", "P@ssword1", 1)] // Last Name empty
        [InlineData("al@walk.com", "Alan", "Alan", "", "P@ssword1", 1)] // Password empty
        [InlineData("al@walk.com", "Alan", "Alan", "P@ssword1", "", 1)] // Confirm Password empty
        [InlineData(null, "Antony", "Hopkins", "TestPassword@1234", "TestPassword@1234", 1)] // Email empty
        [InlineData("notaracist@murica.com", "fname", "lname", "Password", "Pass", 3)] // email is taken && pass not complex && confirm pass doesn't match
        public void PostRegisterDisplaysErrorMessage(string email, string fname, string lname, string password, string confirmPassword, int expectedNumberOfErrors)
        {
            _validations.Clear(); // Just in case

            SendRegistrationModel registrationInfo = new SendRegistrationModel()
            {
                userRegistrationModel = new UserRegistrationModel()
                {
                    Email = email,
                    FirstName = fname,
                    LastName = lname,
                    Password = password,
                    ConfirmPassword = confirmPassword
                },
                ValidationErrorMessages = _validations
            };

            var result = new AccountController(_dbContext).Register(registrationInfo);

            if (expectedNumberOfErrors == 0)
            {
                List<string> persistedEmails = new List<string>();
                foreach (User u in _dbContext.User)
                {
                    persistedEmails.Add(u.Email);
                }
                Assert.Contains<string>(email, persistedEmails);
                Assert.IsType<RedirectToActionResult>(result);
            }
            else
            {
                var requestResult = Assert.IsType<ViewResult>(result);
                var resultModel = Assert.IsAssignableFrom<SendRegistrationModel>(requestResult.ViewData.Model);

                //Verify that the error message is populated on the view's Model
                Assert.True(resultModel.ValidationErrorMessages.Count == expectedNumberOfErrors);

                _validations.Clear();
            }
        }
    }
}

using BoardWebApp.Controllers;
using BoardWebApp.Models;
using BoardWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace BoardWebApp_Tests
{

    public class RegistrationValidationsTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        //private readonly UserRegistrationModel _registrationModel;
        private BoardWebAppContext _dbContext;
        private List<string> _validations;
        public RegistrationValidationsTests(ITestOutputHelper testOutputHelper)
        {
            //_registrationModel = new UserRegistrationModel();
            _validations = new List<string>();
            _output = testOutputHelper;
        }

        public void Dispose()
        {
            //ClearRegistrationModel();
            _validations.Clear();
            _dbContext = null;
        }

        [Theory]
        [InlineData("Antony.Hopkins@Mail.com", null)] //email is not in DB, no error
        [InlineData("notaracist@murica.com", "Email is already in use by another user!")]
        [InlineData("aaaaaaaa", "Please enter a valid email address!")]
        public void EmailValidations(string email, string expectedValidationMessage)
        {
            InitInMemoryDbContext();
            _validations.Clear();

            string actualValidationMessage = null;

            UserRegistrationModel.EmailValidationsStatic(email, _validations, _dbContext);
            if (_validations != null && _validations.Count > 0)
            {
                //_output.WriteLine(@"example {0}", "output");
                actualValidationMessage = _validations[0];
            }
            Assert.Equal(expectedValidationMessage, actualValidationMessage);

            _validations.Clear();
            _dbContext = null;
        }
        
        public void InitInMemoryDbContext()
        {
            Random rng = new Random();

            var optionsBuilder = new DbContextOptionsBuilder<BoardWebAppContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            _dbContext = new BoardWebAppContext(optionsBuilder.Options);
            _dbContext.User.AddRange(
                    new User
                    {
                        Email = "notaracist@murica.com",
                        FirstName = "Zack",
                        LastName = "Yu",
                        Password = "19513fdc9da4fb72a4a05eb66917548d3c90ff94d5419e1f2363eea89dfee1dd",
                        //EmailHash = "e8107daab70f9bec25d249541eb247f36514248a14597b3cdc5ebaa3bb140a68"
                    }, new User
                    {
                        Email = "trudy@board.com",
                        FirstName = "Trudy",
                        LastName = "Turner",
                        Password = "19513fdc9da4fb72a4a05eb66917548d3c90ff94d5419e1f2363eea89dfee1dd",
                        //EmailHash = "08176ba0671827d033a25cfa6608d92caca8008527af3ff32dc23012dc99d554"
                    }
                );
            _dbContext.SaveChanges();
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
            InitInMemoryDbContext();
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
                Assert.IsType<RedirectToActionResult>(result);
            }
            else
            {
                var requestResult = Assert.IsType<ViewResult>(result);
                var resultModel = Assert.IsAssignableFrom<SendRegistrationModel>(requestResult.ViewData.Model);

                //foreach(string s in resultModel.ValidationErrorMessages)
                //{
                //    _output.WriteLine("error: " + s + "\n");
                //}

                //Verify that the error message is populated on the view's Model
                Assert.True(resultModel.ValidationErrorMessages.Count == expectedNumberOfErrors);

                _validations.Clear();
                _dbContext = null;
            }
        }
    }
}

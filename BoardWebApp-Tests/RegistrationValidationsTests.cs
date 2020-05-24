using BoardWebApp.Models;
using BoardWebApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace BoardWebApp_Tests
{

    public class RegistrationValidationsTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly UserRegistrationModel _registrationModel;
        private BoardWebAppContext _dbContext;
        private List<string> _validations;
        public RegistrationValidationsTests(ITestOutputHelper testOutputHelper)
        {
            _registrationModel = new UserRegistrationModel();
            _validations = new List<string>();
            _output = testOutputHelper;   
        }

        public void Dispose()
        {
            ClearRegistrationModel();
            _validations.Clear();
            _dbContext = null;
        }
        public void ClearRegistrationModel()
        {
            _registrationModel.Email = "";
            _registrationModel.FirstName = "";
            _registrationModel.LastName = "";
            _registrationModel.Password = "";
            _registrationModel.ConfirmPassword = "";
        }

        [Theory]
        [InlineData(null,"Antony","Hopkins", "TestPassword@1234", "TestPassword@1234", false)]
        [InlineData("Antony.Hopkins@Mail.com","Antony","Hopkins", "TestPassword@1234", "", false)]
        public void FieldsAreEmptyValidation(string email, string firstName, string lastName, string password, string confirmPassword, bool expectedResult)
        {
            _validations.Clear();
            _registrationModel.Email = email; 
            _registrationModel.FirstName = firstName;
            _registrationModel.LastName = lastName;
            _registrationModel.Password = password;
            _registrationModel.ConfirmPassword = confirmPassword;

            bool actualResult = UserRegistrationModel.FieldsNotEmptyValidationsStatic(_registrationModel, _validations);
            Assert.Equal(expectedResult, actualResult);
            Assert.True(_validations.Count > 0);

            ClearRegistrationModel();
            _validations.Clear();
        }

        [Theory]
        [InlineData("notaracist@murica.com", "Email is already in use by another user!")]
        [InlineData("aaaaaaaa", "Please enter a valid email address!")]
        [InlineData("Antony.Hopkins@Mail.com", null)]

        public void EmailValidations(string email, string expectedValidationMessage)
        {
            InitInMemoryDbContext();
            _validations.Clear();
            UserRegistrationModel.EmailValidationsStatic(email, _validations, _dbContext);
            string actualValidationMessage = null;
            if(_validations != null) {
                _output.WriteLine("validations is not empty: " + _validations.Count);
                if (_validations.Count > 0)
                    {
                        _output.WriteLine(@"validations = {0}", _validations);
                        actualValidationMessage = _validations[0];
                    } 
            }
                else
            {
                _output.WriteLine("Validations is empty");
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
    }
}

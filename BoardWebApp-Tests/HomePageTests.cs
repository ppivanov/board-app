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
    public class HomePageTests : IDisposable
    {
        private BoardWebAppContext _dbContext;
        private readonly ITestOutputHelper _output;
        //private string _mockAuthenticationHash;

        public HomePageTests(ITestOutputHelper testOutputHelper)
        {
            _output = testOutputHelper;
            _dbContext = null;
        }
        public void Dispose()
        {
            _dbContext = null;
        }

        [Theory]
        [InlineData("e8107daab70f9bec25d249541eb247f36514248a14597b3cdc5ebaa3bb140a68c472a2c6627ceb7c0f1b1f1c5e8ed5a903fcede83b4b9da720697c3dc127ffff", true)] // Zack (notaracist@murica.com)
        [InlineData("08176ba0671827d033a25cfa6608d92caca8008527af3ff32dc23012dc99d554217161eda88a0d393461c215f94cdbfb787e82ed7c8e5db2a2bfcbea69c7a0d4", true)] // Trudy (trudy@board.com)
        [InlineData("247f36514248a14597b3cdc5ebaa3bb140a68c472a2c6627ceb7c0f1b1f1c5ehk7d107ebd30c26531e71747343btd42d5f64f6b423a4f9775490s281fceaadfb", false)] // not in db
        public void AuthenticationTest(string mockAuthHash, bool expectedResult)
        {
            _dbContext = RegistrationValidationTests.InitInMemoryDbContext();
            List<string> messages = new List<string>();
            User authenticatedUser = User.AuthenticateBasedOnCookieValue(mockAuthHash, _dbContext);
            bool actualResult = (authenticatedUser != null); // true if a user is found

            Assert.Equal(expectedResult, actualResult);

            _dbContext = null;
        }

        //[Theory]
        //[InlineData("Zack","e8107daab70f9bec25d249541eb247f36514248a14597b3cdc5ebaa3bb140a68","00fcdde26dd77af7858a52e3913e6f3330a32b3121a61bce915cc6145fc44453")]
        //[InlineData("Trudy", "08176ba0671827d033a25cfa6608d92caca8008527af3ff32dc23012dc99d554", "00fcdde26dd77af7858a52e3913e6f3330a32b3121a61bce915cc6145fc44453")]
        //public void Test(string personName, string eHash, string pHash)
        //{
        //    string authHash = User.ComputeSha256HashForString(eHash + pHash);

        //    _output.WriteLine(personName + " - " + authHash);
        //}


    }
}

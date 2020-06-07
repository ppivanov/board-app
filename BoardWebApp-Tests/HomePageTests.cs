using BoardWebApp.Controllers;
using BoardWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using BoardWebApp.Models;

namespace BoardWebApp_Tests
{
    public class HomePageTests : IDisposable
    {
        private BoardWebAppContext _dbContext;
        private readonly ITestOutputHelper _output;

        public HomePageTests(ITestOutputHelper testOutputHelper)
        {
            _output = testOutputHelper;
            _dbContext = InMemoryDb.InitInMemoryDbContext();
        }
        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();;
        }

        // --------------------- This test verifes the authentication method is returning a user whenever the cookie value is matching a User record ---------------------
        // --------------------- Maybe not the place for this test? ---------------------
        [Theory]
        [InlineData("e8107daab70f9bec25d249541eb247f36514248a14597b3cdc5ebaa3bb140a68c472a2c6627ceb7c0f1b1f1c5e8ed5a903fcede83b4b9da720697c3dc127ffff", true)] // Zack (notaracist@murica.com)
        [InlineData("08176ba0671827d033a25cfa6608d92caca8008527af3ff32dc23012dc99d554217161eda88a0d393461c215f94cdbfb787e82ed7c8e5db2a2bfcbea69c7a0d4", true)] // Trudy (trudy@board.com)
        [InlineData("247f36514248a14597b3cdc5ebaa3bb140a68c472a2c6627ceb7c0f1b1f1c5ehk7d107ebd30c26531e71747343btd42d5f64f6b423a4f9775490s281fceaadfb", false)] // not in db
        public void AuthenticationTest(string mockAuthHash, bool expectedResult)
        {
            User authenticatedUser = User.AuthenticateBasedOnCookieValue(mockAuthHash, _dbContext);
            bool actualResult = (authenticatedUser != null); // true if a user is found

            Assert.Equal(expectedResult, actualResult);

        }

        // --------------------- The FOUR tests below are testing the methods retrieving the Boards and Projects a user owns and they're a member of ---------------------
        [Theory]
        [InlineData("notaracist@murica.com", (object)(new string[]{"Zack's shared board"}))]
        [InlineData("trudy@board.com", (object)(new string[]{ "Trudy's board"}))]
        [InlineData("alicia@board.com", (object)(new string[]{ /* doesn't own any boards */ }))]
        public void GetBoardsWhereOwner(string userEmail, string[] expectedBoardNames)
        {
            List<User> allUsers = new List<User>(_dbContext.User);
            User userForEmail = null;
            foreach(User u in allUsers)
            {
                if(u.Email == userEmail)
                    userForEmail = u;
            }
            List<string> actualBoardNames = new List<string>();
            List<Board> actualBoards = userForEmail.GetBoardsWhereThisUserIsOwner(_dbContext);
            foreach(Board actualBoard in actualBoards)
            {
                _output.WriteLine("Actual board name : " + actualBoard.BoardName);
                actualBoardNames.Add(actualBoard.BoardName);
            }

            bool expectedSizeEqActualSize = (expectedBoardNames.Length == actualBoardNames.Count);
            // _output.WriteLine("Expected size : " + expectedBoardNames.Length);
            // _output.WriteLine("Actual size : " + actualBoardNames.Count);
            // _output.WriteLine("result : " + expectedSizeEqActualSize);
            Assert.True(expectedSizeEqActualSize);
            // if the size is not the same, not point trying to verify the board names are the expected ones
            if(expectedSizeEqActualSize == false)
            {
                return;
            } 
            else
            {
                // for each of the expected board names - verify that it's part of the returned List
                foreach(string expected in expectedBoardNames)
                {   
                    _output.WriteLine("Expected board name : " + expected);
                    bool actualListContainsExpectedBoardName = actualBoardNames.Contains(expected);
                    // this isn't working
                    Assert.True(actualListContainsExpectedBoardName);
                }
            }
        }
    }
}

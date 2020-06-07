using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Abstractions;
using Microsoft.AspNetCore.Mvc;
using BoardWebApp.ViewModels;
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
        [InlineData("trudy@board.com", (object)(new string[]{ "Trudy's board", "Trudy's second board"}))]
        [InlineData("alicia@board.com", (object)(new string[]{ /* doesn't own any boards */ }))]
        public void GetBoardsWhereOwner(string userEmail, string[] expectedBoardNames)
        {
            User userForEmail = _dbContext.User.Where(u => u.Email == userEmail).SingleOrDefault();
            List<string> actualBoardNames = new List<string>();
            List<Board> actualBoards = userForEmail.GetBoardsWhereThisUserIsOwner(_dbContext); // gets the boards
            // add the boards' name to the list
            foreach(Board actualBoard in actualBoards)
            {
                actualBoardNames.Add(actualBoard.BoardName);
            }

            bool expectedSizeEqActualSize = (expectedBoardNames.Length == actualBoardNames.Count);
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
                    bool actualListContainsExpectedBoardName = actualBoardNames.Contains(expected);
                    Assert.True(actualListContainsExpectedBoardName);
                }
            }
        }
        [Theory]
        [InlineData("notaracist@murica.com", (object)(new string[]{/* Not a member of any board */}))]
        [InlineData("trudy@board.com", (object)(new string[]{ "Zack's shared board"}))]
        [InlineData("alicia@board.com", (object)(new string[]{ "Zack's shared board", "Trudy's second board" }))]
        public void GetBoardsWhereMember(string userEmail, string[] expectedBoardNames)
        {
            User userForEmail = _dbContext.User.Where(u => u.Email == userEmail).SingleOrDefault();
            List<string> actualBoardNames = new List<string>();
            List<Board> actualBoards = userForEmail.GetBoardsWhereThisUserIsMember(_dbContext); // gets the boards
            // add the boards' name to the list
            foreach(Board actualBoard in actualBoards)
            {
                actualBoardNames.Add(actualBoard.BoardName);
            }

            bool expectedSizeEqActualSize = (expectedBoardNames.Length == actualBoardNames.Count);
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
                    bool actualListContainsExpectedBoardName = actualBoardNames.Contains(expected);
                    Assert.True(actualListContainsExpectedBoardName);
                }
            }
        }
        [Theory]
        [InlineData("notaracist@murica.com", (object)(new string[]{ "New App" }))]
        [InlineData("trudy@board.com", (object)(new string[]{ /* Trudy's the owner */ }))]
        [InlineData("alicia@board.com", (object)(new string[]{ /* Not a member of any project */ }))]
        public void GetProjectsWhereMember(string userEmail, string[] expectedProjectNames)
        {
            User userForEmail = _dbContext.User.Where(u => u.Email == userEmail).SingleOrDefault();
            List<string> actualProjectNames = new List<string>();
            List<Project> actualProjects = userForEmail.GetProjectsWhereThisUserIsMember(_dbContext); // gets the projects
            // add the project's' name to the list
            foreach(Project actualProject in actualProjects)
            {
                actualProjectNames.Add(actualProject.ProjectName);
            }

            bool expectedSizeEqActualSize = (expectedProjectNames.Length == actualProjectNames.Count);
            Assert.True(expectedSizeEqActualSize);
            // if the size is not the same, not point trying to verify the project names are the expected ones
            if(expectedSizeEqActualSize == false)
            {
                return;
            } 
            else
            {
                // for each of the expected project names - verify that it's part of the returned List
                foreach(string expected in expectedProjectNames)
                {   
                    bool actualListContainsExpectedProjectName = actualProjectNames.Contains(expected);
                    Assert.True(actualListContainsExpectedProjectName);
                }
            }
        }
        [Theory]
        [InlineData("notaracist@murica.com", (object)(new string[]{ /* Does not own any projects */ }))]
        [InlineData("trudy@board.com", (object)(new string[]{ "New App" }))]
        [InlineData("alicia@board.com", (object)(new string[]{ /* Does not own any projects */ }))]
        public void GetProjectsWhereOwner(string userEmail, string[] expectedProjectNames)
        {
            User userForEmail = _dbContext.User.Where(u => u.Email == userEmail).SingleOrDefault();
            List<string> actualProjectNames = new List<string>();
            List<Project> actualProjects = userForEmail.GetProjectsWhereThisUserIsOwner(_dbContext); // gets the projects
            // add the project's' name to the list
            foreach(Project actualProject in actualProjects)
            {
                actualProjectNames.Add(actualProject.ProjectName);
            }

            bool expectedSizeEqActualSize = (expectedProjectNames.Length == actualProjectNames.Count);
            Assert.True(expectedSizeEqActualSize);
            // if the size is not the same, not point trying to verify the project names are the expected ones
            if(expectedSizeEqActualSize == false)
            {
                return;
            } 
            else
            {
                // for each of the expected project names - verify that it's part of the returned List
                foreach(string expected in expectedProjectNames)
                {   
                    bool actualListContainsExpectedProjectName = actualProjectNames.Contains(expected);
                    Assert.True(actualListContainsExpectedProjectName);
                }
            }
        }
    }
}

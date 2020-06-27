using System;
using Microsoft.EntityFrameworkCore;
using BoardWebApp.Models;

namespace BoardWebApp_Tests
{
    public static class InMemoryDb
    {
        public static BoardWebAppContext InitInMemoryDbContext()
        {
            Random rng = new Random();

            var optionsBuilder = new DbContextOptionsBuilder<BoardWebAppContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            BoardWebAppContext dbContext = new BoardWebAppContext(optionsBuilder.Options);
            dbContext.User.AddRange(
                new User
                {
                    UserId = 1,
                    Email = "trudy@board.com",
                    FirstName = "Trudy",
                    LastName = "Turner",
                    Password = "00fcdde26dd77af7858a52e3913e6f3330a32b3121a61bce915cc6145fc44453",
                    EmailHash = "08176ba0671827d033a25cfa6608d92caca8008527af3ff32dc23012dc99d554"
                    // cookie value = 08176ba0671827d033a25cfa6608d92caca8008527af3ff32dc23012dc99d554217161eda88a0d393461c215f94cdbfb787e82ed7c8e5db2a2bfcbea69c7a0d4
                },
                new User
                {
                    UserId = 2,
                    Email = "notaracist@murica.com",
                    FirstName = "Zack",
                    LastName = "Yu",
                    Password = "00fcdde26dd77af7858a52e3913e6f3330a32b3121a61bce915cc6145fc44453",
                    EmailHash = "e8107daab70f9bec25d249541eb247f36514248a14597b3cdc5ebaa3bb140a68"
                    // cookie value = e8107daab70f9bec25d249541eb247f36514248a14597b3cdc5ebaa3bb140a68c472a2c6627ceb7c0f1b1f1c5e8ed5a903fcede83b4b9da720697c3dc127ffff
                },
                new User
                {
                    UserId = 3,
                    Email = "alicia@board.com",
                    FirstName = "Alicia",
                    LastName = "Keys",
                    Password = "00fcdde26dd77af7858a52e3913e6f3330a32b3121a61bce915cc6145fc44453",
                    EmailHash = "ed0f7a304e3f1337db52d0e5f3087cd02ef4e16f4665274b23f7b423f56b4180"
                    // cookie value = ed0f7a304e3f1337db52d0e5f3087cd02ef4e16f4665274b23f7b423f56b4180e0860fdd8998c527f848820d1d0be2b07b42c0a8c54ba49a31f70888a436daad
                }
            );
            dbContext.Project.AddRange(
                new Project
                {
                    ProjectId = 1,
                    ProjectName = "New App",
                    ProjectDescription = "Creating a new App"
                }
            );
            dbContext.BoardType.AddRange(
                new BoardType { BoardTypeId = 1, TypeName = "Sprint Board"} ,
                new BoardType { BoardTypeId = 2, TypeName = "Backlog" },
                new BoardType { BoardTypeId = 3, TypeName = "History Board" }
            );
            dbContext.UserMemberType.AddRange(
                new UserMemberType { MemberTypeId = 1, MemberType = "Owner" },
                new UserMemberType { MemberTypeId = 2, MemberType = "Member" },
                new UserMemberType { MemberTypeId = 3, MemberType = "Scrum Master" }
            );
            dbContext.Board.AddRange(
                new Board
                {
                    BoardId = 1,
                    BoardName = "[New App] - Sprint Board",
                    BoardDescription = "This is the sprint board for the project New App.",
                    BoardTypeId = 1,
                    ProjectId = 1
                },
                new Board
                {
                    BoardId = 2,
                    BoardName = "[New App] - Backlog",
                    BoardDescription = "This is the backlog for the project New App.",
                    BoardTypeId = 2,
                    ProjectId = 1
                },
                new Board
                {
                    BoardId = 3,
                    BoardName = "[New App] - History Board",
                    BoardDescription = "This is the history board for the project New App.",
                    BoardTypeId = 3,
                    ProjectId = 1
                },
                new Board
                {
                    BoardId = 4,
                    BoardName = "Zack's shared board",
                    BoardDescription = "Zack's board"
                },
                new Board
                {
                    BoardId = 5,
                    BoardName = "Trudy's board",
                    BoardDescription = "Trudy's board"
                },
                new Board
                {
                    BoardId = 6,
                    BoardName = "Trudy's second board",
                    BoardDescription = "Trudy's second board"
                }
            );  
            dbContext.BoardColumn.AddRange(
                new BoardColumn { ColumnId = 1, ColumnName = "Up next", ColumnOrder = 1, BoardId = 1 },
                new BoardColumn { ColumnId = 2, ColumnName = "In Progress", ColumnOrder = 2, BoardId = 1 },
                new BoardColumn { ColumnId = 3, ColumnName = "Done", ColumnOrder = 3, BoardId = 1 },
                new BoardColumn { ColumnId = 4, ColumnName = "Stash", ColumnOrder = 1, BoardId = 2 },
                new BoardColumn { ColumnId = 5, ColumnName = "Sprint Jan", ColumnOrder = 1, BoardId = 3 },
                new BoardColumn { ColumnId = 6, ColumnName = "Sprint Feb", ColumnOrder = 2, BoardId = 3 },
                new BoardColumn { ColumnId = 7, ColumnName = "Sprint Mar", ColumnOrder = 3, BoardId = 3 },
                new BoardColumn { ColumnId = 8, ColumnName = "Zack", ColumnOrder = 1, BoardId = 4 },
                new BoardColumn { ColumnId = 9, ColumnName = "Trudy", ColumnOrder = 1, BoardId = 5 },
                new BoardColumn { ColumnId = 10, ColumnName = "Trudy 2", ColumnOrder = 1, BoardId = 6 }
            );
            dbContext.ProjectMember.AddRange(
                new ProjectMember{ ProjectId = 1, MemberId = 1, MemberTypeId = 1 }, // Trudy as the owner of 'New App'
                new ProjectMember{ ProjectId = 1, MemberId = 2, MemberTypeId = 2 } // Zack as a member of 'New App's
            );
            dbContext.BoardMember.AddRange(
                new BoardMember{ BoardId = 4, MemberId = 2, MemberTypeId = 1 }, // Zack's shared board - Zack as owner
                new BoardMember{ BoardId = 4, MemberId = 1, MemberTypeId = 2 }, // Zack's shared board - Trudy as member
                new BoardMember{ BoardId = 4, MemberId = 3, MemberTypeId = 2 }, // Zack's shared board - Alicia as member
                new BoardMember{ BoardId = 5, MemberId = 1, MemberTypeId = 1 }, // Trudy's board - Trudy as owner
                new BoardMember{ BoardId = 6, MemberId = 1, MemberTypeId = 1 }, // Trudy's second board - Trudy as owner
                new BoardMember{ BoardId = 6, MemberId = 3, MemberTypeId = 2 } // Trudy's second board - Alicia as member
            );
            
            //TicketType
            //Ticket
            //Subtask
            //Notification
            dbContext.SaveChanges();
            return dbContext;
        }
    }
}
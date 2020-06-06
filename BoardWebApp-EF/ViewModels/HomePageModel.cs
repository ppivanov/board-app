using System;
using System.Collections.Generic;
using System.Linq;
// using System.Linq;
using System.Threading.Tasks;
using BoardWebApp.Models;

namespace BoardWebApp.ViewModels
{
    public class HomePageModel
    {
        public List<Board> OwnedBoards { get; set; }
        public List<Board> MemberAccessToBoards { get; set; }
        public List<Project> OwnedProjects { get; set; }
        public List<Project> MemberAccessToProjects { get; set; }
        public string Message { get; set; } // used to display the 'Login successful!' message.

        /***** Add more data you want to display on the home page here *****/

        public HomePageModel()
        {
         // default constructor // all values will be null/empty    
        }

        public HomePageModel(string message)
        {
            Message = message;
        }

        public HomePageModel(User authenticatedUser, BoardWebAppContext dbContext)
        {
            // notifications here?

            OwnedBoards = authenticatedUser.GetBoardsWhereThisUserIsOwner(dbContext);

            MemberAccessToBoards = authenticatedUser.GetBoardsWhereThisUserIsMember(dbContext);

            OwnedProjects = authenticatedUser.GetProjectsWhereThisUserIsOwner(dbContext);

            MemberAccessToProjects = authenticatedUser.GetProjectsWhereThisUserIsMember(dbContext);
        }

       
    }
}

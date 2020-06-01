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
            //Member Type - Id
            //  Owner         - 1
            //  Member        - 2
            //  Scrum Master  - 3

            // populates the lists with the user's related boards and projects
            // notifications here?

            // Get the IDs of the boards where the user is the owner.
            List<int> boardIdsWhereOwner = dbContext.ProjectMember.Where(pm => pm.MemberId == authenticatedUser.UserId && pm.MemberTypeId == 1).Select(pm => pm.ProjectId).ToList();
            // Get the projects from the BoardIds retrieved above.
            OwnedProjects = dbContext.Project.Where(p => boardIdsWhereOwner.Contains(p.ProjectId))
                                            .ToList();

            // Get the IDs of the boards where the user is a member.
            List<int> boardIdsWhereMember = dbContext.ProjectMember.Where(pm => pm.MemberId == authenticatedUser.UserId && pm.MemberTypeId == 2).Select(pm => pm.ProjectId).ToList();
            // Get the projects from the BoardIds retrieved above.
            MemberAccessToProjects = dbContext.Project.Where(p => boardIdsWhereMember.Contains(p.ProjectId))
                                            .ToList();

            List<int> projectIdsWhereOwner = dbContext.ProjectMember.Where(pm => pm.MemberId == authenticatedUser.UserId && pm.MemberTypeId == 1).Select(pm => pm.ProjectId).ToList();
            OwnedProjects = dbContext.Project.Where(p => projectIdsWhereOwner.Contains(p.ProjectId))
                                            .ToList();
            
            List<int> projectIdsWhereMember = dbContext.ProjectMember.Where(pm => pm.MemberId == authenticatedUser.UserId && pm.MemberTypeId == 2).Select(pm => pm.ProjectId).ToList();
            MemberAccessToProjects = dbContext.Project.Where(p => projectIdsWhereMember.Contains(p.ProjectId))
                                            .ToList();
        }

    }
}

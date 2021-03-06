﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BoardWebApp.Models
{
    public partial class User
    {
        public User()
        {
            //Board = new HashSet<Board>();
            BoardMember = new HashSet<BoardMember>();
            NotificationForUser = new HashSet<Notification>();
            NotificationFromUser = new HashSet<Notification>();
            ProjectMember = new HashSet<ProjectMember>();
            //ProjectProjectOwner = new HashSet<Project>();
            //ProjectProjectScrumMaster = new HashSet<Project>();
            Ticket = new HashSet<Ticket>();
        }

        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string EmailHash { get; set; }

        //public virtual ICollection<Board> Board { get; set; }
        public virtual ICollection<BoardMember> BoardMember { get; set; }
        public virtual ICollection<Notification> NotificationForUser { get; set; }
        public virtual ICollection<Notification> NotificationFromUser { get; set; }
        public virtual ICollection<ProjectMember> ProjectMember { get; set; }
        //public virtual ICollection<Project> ProjectProjectOwner { get; set; }
        //public virtual ICollection<Project> ProjectProjectScrumMaster { get; set; }
        public virtual ICollection<Ticket> Ticket { get; set; }


        public static User AuthenticateBasedOnCookieValue(string cookieValue, BoardWebAppContext dbContext)
        {
            if(cookieValue != null){
                string emailHash = cookieValue.Substring(0, 64); // first 64 characters of the cookie's value
                string authenticationHash = cookieValue.Substring(64); // every character after 64th.
                User userFromQuery = dbContext.User.Where(user => user.EmailHash == emailHash).FirstOrDefault(); // query the DB and retrieve the user if found.
                
                if(userFromQuery != null)
                {
                    // Compute the hash for the User DB record that was found.
                    string computedHashForUser = ComputeSha256HashForString(userFromQuery.EmailHash + userFromQuery.Password);
                    // compare computed against incoming hash. If there's a match - return the user.
                    if(authenticationHash == computedHashForUser)
                    {
                        return userFromQuery;
                    }
                }
            }
            return null;
        }

        public static string ComputeSha256HashForString(string rawString)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawString));
                StringBuilder hashedString = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    hashedString.Append(bytes[i].ToString("x2"));
                }
                return hashedString.ToString();
            } 
        }


        //  Member Type   - Id
        //  Owner         - 1
        //  Member        - 2
        //  Scrum Master  - 3

        // Returns the boards this user owns.
        public List<Board> GetBoardsWhereThisUserIsOwner(BoardWebAppContext dbContext)
        {
            // Get the IDs of the boards where the user is the owner.
            List<int> boardIdsWhereOwner = dbContext.BoardMember.Where(bm => bm.MemberId == UserId && bm.MemberTypeId == 1).Select(bm => bm.BoardId).ToList();
            // Get the boards from the BoardIds retrieved above.
            List<Board> ownedBoards = dbContext.Board.Where(p => boardIdsWhereOwner.Contains(p.BoardId))
                                            .ToList();
            
            return ownedBoards;
        }
        
        // Returns the boards this user is a member of but does not own.
        public List<Board> GetBoardsWhereThisUserIsMember(BoardWebAppContext dbContext)
        {
            // Get the IDs of the boards where the user is a member.
            List<int> boardIdsWhereMember = dbContext.BoardMember.Where(bm => bm.MemberId == UserId && bm.MemberTypeId == 2).Select(bm => bm.BoardId).ToList();
            // Get the projects from the BoardIds retrieved above.
            List<Board> boardsWhereUserIsMember = dbContext.Board.Where(b => boardIdsWhereMember.Contains(b.BoardId))
                                            .ToList();

            return boardsWhereUserIsMember;
        } 
        
        // Returns the projects this user owns.
        public List<Project> GetProjectsWhereThisUserIsOwner(BoardWebAppContext dbContext)
        {
            List<int> projectIdsWhereOwner = dbContext.ProjectMember.Where(pm => pm.MemberId == UserId && pm.MemberTypeId == 1).Select(pm => pm.ProjectId).ToList();
            List<Project> ownedProjects = dbContext.Project.Where(p => projectIdsWhereOwner.Contains(p.ProjectId))
                                                .ToList();
            return ownedProjects;
        }
        // Returns the projects this user is a member of but does not own.
        public List<Project> GetProjectsWhereThisUserIsMember(BoardWebAppContext dbContext)
        {
            List<int> projectIdsWhereMember = dbContext.ProjectMember.Where(pm => pm.MemberId == UserId && pm.MemberTypeId == 2).Select(pm => pm.ProjectId).ToList();
            List<Project> projectsWhereUserIsMember = dbContext.Project.Where(p => projectIdsWhereMember.Contains(p.ProjectId))
                                            .ToList();

            return projectsWhereUserIsMember;
        }

    }
}

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
            Board = new HashSet<Board>();
            BoardMember = new HashSet<BoardMember>();
            NotificationForUser = new HashSet<Notification>();
            NotificationFromUser = new HashSet<Notification>();
            ProjectMember = new HashSet<ProjectMember>();
            ProjectProjectOwner = new HashSet<Project>();
            ProjectProjectScrumMaster = new HashSet<Project>();
            Ticket = new HashSet<Ticket>();
        }

        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string EmailHash { get; set; }

        public virtual ICollection<Board> Board { get; set; }
        public virtual ICollection<BoardMember> BoardMember { get; set; }
        public virtual ICollection<Notification> NotificationForUser { get; set; }
        public virtual ICollection<Notification> NotificationFromUser { get; set; }
        public virtual ICollection<ProjectMember> ProjectMember { get; set; }
        public virtual ICollection<Project> ProjectProjectOwner { get; set; }
        public virtual ICollection<Project> ProjectProjectScrumMaster { get; set; }
        public virtual ICollection<Ticket> Ticket { get; set; }


        public static User AuthenticateBasedOnCookieValue(string cookieValue, BoardWebAppContext dbContext)
        {
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
                
    }
}

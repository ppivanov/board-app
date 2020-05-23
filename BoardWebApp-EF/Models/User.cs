using System;
using System.Collections.Generic;
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

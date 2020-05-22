using System;
using System.Collections.Generic;

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

        public virtual ICollection<Board> Board { get; set; }
        public virtual ICollection<BoardMember> BoardMember { get; set; }
        public virtual ICollection<Notification> NotificationForUser { get; set; }
        public virtual ICollection<Notification> NotificationFromUser { get; set; }
        public virtual ICollection<ProjectMember> ProjectMember { get; set; }
        public virtual ICollection<Project> ProjectProjectOwner { get; set; }
        public virtual ICollection<Project> ProjectProjectScrumMaster { get; set; }
        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}

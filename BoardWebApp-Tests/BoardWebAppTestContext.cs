using BoardWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BoardWebApp_Tests
{
    public class BoardWebAppTestContext : DbContext
    {
        public BoardWebAppTestContext()
        {
        }

        public BoardWebAppTestContext(DbContextOptions<BoardWebAppTestContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Board> Board { get; set; }
        public virtual DbSet<BoardColumn> BoardColumn { get; set; }
        public virtual DbSet<BoardMember> BoardMember { get; set; }
        public virtual DbSet<BoardType> BoardType { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<ProjectMember> ProjectMember { get; set; }
        public virtual DbSet<Subtask> Subtask { get; set; }
        public virtual DbSet<Ticket> Ticket { get; set; }
        public virtual DbSet<TicketType> TicketType { get; set; }
        public virtual DbSet<User> User { get; set; }

        
    }
}

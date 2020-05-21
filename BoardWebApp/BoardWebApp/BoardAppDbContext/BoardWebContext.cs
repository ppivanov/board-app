using System;
using BoardWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BoardAppDbContext.DB
{
    public class BoardWepContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Data Source=blogging.db");
        

        // Database Tables
        public DbSet<Board> Boards { get; set; }
        public DbSet<BoardColumn> Columns { get; set; }
        public DbSet<BoardType> Types { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<User> User { get; set; }
    }
}

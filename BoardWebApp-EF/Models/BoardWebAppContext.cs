using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BoardWebApp.Models
{
    public partial class BoardWebAppContext : DbContext
    {
        public BoardWebAppContext()
        {
        }

        public BoardWebAppContext(DbContextOptions<BoardWebAppContext> options)
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
        public virtual DbSet<UserMemberType> UserMemberType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BoardWebApp;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Board>(entity =>
            {
                entity.Property(e => e.BoardId).HasColumnName("board_id");

                entity.Property(e => e.BoardDescription)
                    .IsRequired()
                    .HasColumnName("board_description")
                    .HasMaxLength(255);

                entity.Property(e => e.BoardName)
                    .IsRequired()
                    .HasColumnName("board_name")
                    .HasMaxLength(255);

                entity.Property(e => e.BoardTypeId).HasColumnName("board_type_id");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.HasOne(d => d.BoardType)
                    .WithMany(p => p.Board)
                    .HasForeignKey(d => d.BoardTypeId)
                    .HasConstraintName("FK__Board__board_typ__61DB776A");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Board)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK__Board__project_i__62CF9BA3");
            });

            modelBuilder.Entity<BoardColumn>(entity =>
            {
                entity.HasKey(e => e.ColumnId)
                    .HasName("PK__Board_Co__E301851F6A81FB3F");

                entity.ToTable("Board_Column");

                entity.Property(e => e.ColumnId).HasColumnName("column_id");

                entity.Property(e => e.ArchiveDate)
                    .HasColumnName("archive_date")
                    .HasColumnType("date");

                entity.Property(e => e.BoardId).HasColumnName("board_id");

                entity.Property(e => e.ColumnName)
                    .IsRequired()
                    .HasColumnName("column_name")
                    .HasMaxLength(255);

                entity.Property(e => e.ColumnOrder).HasColumnName("column_order");

                entity.HasOne(d => d.Board)
                    .WithMany(p => p.BoardColumn)
                    .HasForeignKey(d => d.BoardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Board_Col__board__697C9932");
            });

            modelBuilder.Entity<BoardMember>(entity =>
            {
                entity.HasKey(e => new { e.BoardId, e.MemberId })
                    .HasName("PK__Board_Me__A0352EBAFC94EC3F");

                entity.ToTable("Board_Member");

                entity.Property(e => e.BoardId).HasColumnName("board_id");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.MemberTypeId).HasColumnName("member_type_id");

                entity.HasOne(d => d.Board)
                    .WithMany(p => p.BoardMember)
                    .HasForeignKey(d => d.BoardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Board_Mem__board__65AC084E");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.BoardMember)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Board_Mem__membe__66A02C87");
            });

            modelBuilder.Entity<BoardType>(entity =>
            {
                entity.ToTable("Board_Type");

                entity.Property(e => e.BoardTypeId).HasColumnName("board_type_id");

                entity.Property(e => e.TypeName)
                    .IsRequired()
                    .HasColumnName("type_name")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(e => e.NotificationId).HasColumnName("notification_id");

                entity.Property(e => e.ForUserId).HasColumnName("for_user_id");

                entity.Property(e => e.FromUserId).HasColumnName("from_user_id");

                entity.Property(e => e.NotificationText)
                    .IsRequired()
                    .HasColumnName("notification_text")
                    .HasMaxLength(500);

                entity.HasOne(d => d.ForUser)
                    .WithMany(p => p.NotificationForUser)
                    .HasForeignKey(d => d.ForUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Notificat__for_u__63C3BFDC");

                entity.HasOne(d => d.FromUser)
                    .WithMany(p => p.NotificationFromUser)
                    .HasForeignKey(d => d.FromUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Notificat__from___64B7E415");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.ProjectDescription)
                    .IsRequired()
                    .HasColumnName("project_description")
                    .HasMaxLength(255);

                entity.Property(e => e.ProjectName)
                    .IsRequired()
                    .HasColumnName("project_name")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<ProjectMember>(entity =>
            {
                entity.HasKey(e => new { e.ProjectId, e.MemberId })
                    .HasName("PK__Project___E750264C926D7A51");

                entity.ToTable("Project_Member");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.MemberTypeId).HasColumnName("member_type_id");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.ProjectMember)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Project_M__membe__688874F9");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectMember)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Project_M__proje__679450C0");
            });

            modelBuilder.Entity<Subtask>(entity =>
            {
                entity.Property(e => e.SubtaskId).HasColumnName("subtask_id");

                entity.Property(e => e.SubtaskDescription)
                    .IsRequired()
                    .HasColumnName("subtask_description")
                    .HasMaxLength(255);

                entity.Property(e => e.SubtaskDone).HasColumnName("subtask_done");

                entity.Property(e => e.SubtaskOrder).HasColumnName("subtask_order");

                entity.Property(e => e.TicketId).HasColumnName("ticket_id");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.Subtask)
                    .HasForeignKey(d => d.TicketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Subtask__ticket___6D4D2A16");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.Property(e => e.TicketId).HasColumnName("ticket_id");

                entity.Property(e => e.AssigneeId).HasColumnName("assignee_id");

                entity.Property(e => e.ColumnId).HasColumnName("column_id");

                entity.Property(e => e.TicketDescription)
                    .IsRequired()
                    .HasColumnName("ticket_description")
                    .HasMaxLength(255);

                entity.Property(e => e.TicketOrder).HasColumnName("ticket_order");

                entity.Property(e => e.TicketTitle)
                    .IsRequired()
                    .HasColumnName("ticket_title")
                    .HasMaxLength(255);

                entity.Property(e => e.TicketTypeId).HasColumnName("ticket_type_id");

                entity.HasOne(d => d.Assignee)
                    .WithMany(p => p.Ticket)
                    .HasForeignKey(d => d.AssigneeId)
                    .HasConstraintName("FK__Ticket__assignee__6C5905DD");

                entity.HasOne(d => d.Column)
                    .WithMany(p => p.Ticket)
                    .HasForeignKey(d => d.ColumnId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ticket__column_i__6A70BD6B");

                entity.HasOne(d => d.TicketType)
                    .WithMany(p => p.Ticket)
                    .HasForeignKey(d => d.TicketTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ticket__ticket_t__6B64E1A4");
            });

            modelBuilder.Entity<TicketType>(entity =>
            {
                entity.ToTable("Ticket_Type");

                entity.Property(e => e.TicketTypeId).HasColumnName("ticket_type_id");

                entity.Property(e => e.TicketType1)
                    .IsRequired()
                    .HasColumnName("ticket_type")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(255);

                entity.Property(e => e.EmailHash)
                    .IsRequired()
                    .HasColumnName("email_hash")
                    .HasMaxLength(499);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(255);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(255);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(499);
            });

            modelBuilder.Entity<UserMemberType>(entity =>
            {
                entity.HasKey(e => e.MemberTypeId)
                    .HasName("PK__User_Mem__510D41A64C3692DA");

                entity.ToTable("User_Member_Type");

                entity.Property(e => e.MemberTypeId).HasColumnName("member_type_id");

                entity.Property(e => e.MemberType)
                    .IsRequired()
                    .HasColumnName("member_type")
                    .HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

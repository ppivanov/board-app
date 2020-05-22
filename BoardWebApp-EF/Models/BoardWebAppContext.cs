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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("");
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

                entity.Property(e => e.BoardOwnerId).HasColumnName("board_owner_id");

                entity.Property(e => e.BoardTypeId).HasColumnName("board_type_id");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.HasOne(d => d.BoardOwner)
                    .WithMany(p => p.Board)
                    .HasForeignKey(d => d.BoardOwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Board__board_own__3B60C8C7");

                entity.HasOne(d => d.BoardType)
                    .WithMany(p => p.Board)
                    .HasForeignKey(d => d.BoardTypeId)
                    .HasConstraintName("FK__Board__board_typ__3C54ED00");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Board)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK__Board__project_i__3D491139");
            });

            modelBuilder.Entity<BoardColumn>(entity =>
            {
                entity.HasKey(e => e.ColumnId)
                    .HasName("PK__Board_Co__E301851FB3A4F08C");

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
                    .HasConstraintName("FK__Board_Col__board__45DE573A");
            });

            modelBuilder.Entity<BoardMember>(entity =>
            {
                entity.HasKey(e => new { e.BoardId, e.MemberId })
                    .HasName("PK__Board_Me__A0352EBA7CBF56CB");

                entity.ToTable("Board_Member");

                entity.Property(e => e.BoardId).HasColumnName("board_id");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.HasOne(d => d.Board)
                    .WithMany(p => p.BoardMember)
                    .HasForeignKey(d => d.BoardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Board_Mem__board__4119A21D");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.BoardMember)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Board_Mem__membe__40257DE4");
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
                    .HasConstraintName("FK__Notificat__for_u__3E3D3572");

                entity.HasOne(d => d.FromUser)
                    .WithMany(p => p.NotificationFromUser)
                    .HasForeignKey(d => d.FromUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Notificat__from___3F3159AB");
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

                entity.Property(e => e.ProjectOwnerId).HasColumnName("project_owner_id");

                entity.Property(e => e.ProjectScrumMasterId).HasColumnName("project_scrum_master_id");

                entity.HasOne(d => d.ProjectOwner)
                    .WithMany(p => p.ProjectProjectOwner)
                    .HasForeignKey(d => d.ProjectOwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Project__project__420DC656");

                entity.HasOne(d => d.ProjectScrumMaster)
                    .WithMany(p => p.ProjectProjectScrumMaster)
                    .HasForeignKey(d => d.ProjectScrumMasterId)
                    .HasConstraintName("FK__Project__project__4301EA8F");
            });

            modelBuilder.Entity<ProjectMember>(entity =>
            {
                entity.HasKey(e => new { e.ProjectId, e.MemberId })
                    .HasName("PK__Project___E750264CE76C276B");

                entity.ToTable("Project_Member");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.ProjectMember)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Project_M__membe__44EA3301");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectMember)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Project_M__proje__43F60EC8");
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
                    .HasConstraintName("FK__Subtask__ticket___49AEE81E");
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
                    .HasConstraintName("FK__Ticket__assignee__48BAC3E5");

                entity.HasOne(d => d.Column)
                    .WithMany(p => p.Ticket)
                    .HasForeignKey(d => d.ColumnId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ticket__column_i__46D27B73");

                entity.HasOne(d => d.TicketType)
                    .WithMany(p => p.Ticket)
                    .HasForeignKey(d => d.TicketTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ticket__ticket_t__47C69FAC");
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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

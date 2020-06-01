using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardWebApp.Models
{
    public partial class Project
    {
        public Project()
        {
            Board = new HashSet<Board>();
            ProjectMember = new HashSet<ProjectMember>();
        }

        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }

        [InverseProperty("Project")]
        public virtual ICollection<Board> Board { get; set; }
        [InverseProperty("Project")]
        public virtual ICollection<ProjectMember> ProjectMember { get; set; }
    }
}

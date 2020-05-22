using System;
using System.Collections.Generic;

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
        public int ProjectOwnerId { get; set; }
        public int? ProjectScrumMasterId { get; set; }

        public virtual User ProjectOwner { get; set; }
        public virtual User ProjectScrumMaster { get; set; }
        public virtual ICollection<Board> Board { get; set; }
        public virtual ICollection<ProjectMember> ProjectMember { get; set; }
    }
}

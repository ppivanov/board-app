using System;
using System.Collections.Generic;

namespace BoardWebApp.Models
{
    public partial class UserMemberType
    {
        public UserMemberType()
        {
            BoardMember = new HashSet<BoardMember>();
            ProjectMember = new HashSet<ProjectMember>();
        }

        public int MemberTypeId { get; set; }
        public string MemberType { get; set; }

        public virtual ICollection<BoardMember> BoardMember { get; set; }
        public virtual ICollection<ProjectMember> ProjectMember { get; set; }
    }
}

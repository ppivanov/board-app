using System;
using System.Collections.Generic;

namespace BoardWebApp.Models
{
    public partial class ProjectMember
    {
        public int ProjectId { get; set; }
        public int MemberId { get; set; }
        public int MemberTypeId { get; set; }

        public virtual UserMemberType Member { get; set; }
        public virtual Project Project { get; set; }
    }
}

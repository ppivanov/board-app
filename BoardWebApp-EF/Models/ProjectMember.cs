using System;
using System.Collections.Generic;

namespace BoardWebApp.Models
{
    public partial class ProjectMember
    {
        public int ProjectId { get; set; }
        public int MemberId { get; set; }

        public virtual User Member { get; set; }
        public virtual Project Project { get; set; }
    }
}

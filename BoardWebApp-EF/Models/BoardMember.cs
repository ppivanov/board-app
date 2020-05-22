using System;
using System.Collections.Generic;

namespace BoardWebApp.Models
{
    public partial class BoardMember
    {
        public int BoardId { get; set; }
        public int MemberId { get; set; }

        public virtual Board Board { get; set; }
        public virtual User Member { get; set; }
    }
}

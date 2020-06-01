using System;
using System.Collections.Generic;

namespace BoardWebApp.Models
{
    public partial class BoardMember
    {
        public int BoardId { get; set; }
        public int MemberId { get; set; }
        public int MemberTypeId { get; set; }

        public virtual Board Board { get; set; }
        public virtual UserMemberType Member { get; set; }
    }
}

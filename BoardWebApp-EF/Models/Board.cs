using System;
using System.Collections.Generic;

namespace BoardWebApp.Models
{
    public partial class Board
    {
        public Board()
        {
            BoardColumn = new HashSet<BoardColumn>();
            BoardMember = new HashSet<BoardMember>();
        }

        public int BoardId { get; set; }
        public string BoardName { get; set; }
        public string BoardDescription { get; set; }
        public int BoardOwnerId { get; set; }
        public int? BoardTypeId { get; set; }
        public int? ProjectId { get; set; }

        public virtual User BoardOwner { get; set; }
        public virtual BoardType BoardType { get; set; }
        public virtual Project Project { get; set; }
        public virtual ICollection<BoardColumn> BoardColumn { get; set; }
        public virtual ICollection<BoardMember> BoardMember { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace BoardWebApp.Models
{
    public partial class BoardType
    {
        public BoardType()
        {
            Board = new HashSet<Board>();
        }

        public int BoardTypeId { get; set; }
        public string TypeName { get; set; }

        public virtual ICollection<Board> Board { get; set; }
    }
}

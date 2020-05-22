using System;
using System.Collections.Generic;

namespace BoardWebApp.Models
{
    public partial class BoardColumn
    {
        public BoardColumn()
        {
            Ticket = new HashSet<Ticket>();
        }

        public int ColumnId { get; set; }
        public string ColumnName { get; set; }
        public int ColumnOrder { get; set; }
        public DateTime? ArchiveDate { get; set; }
        public int BoardId { get; set; }

        public virtual Board Board { get; set; }
        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}

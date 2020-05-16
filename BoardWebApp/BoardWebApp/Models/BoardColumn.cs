using System;
using System.Collections.Generic;

namespace BoardWebApp.Models
{
    public class BoardColumn
    {
        public int ColumnId { get; set; }
        public string ColumnName { get; set; }
        public int ColumnOrder { get; set; }
        public DateTime ArchiveDate { get; set; }
        // public Board MyBoard { get; set; }
        public List<Ticket> Tickets { get; set; }

        public BoardColumn()
        {
        }
    }
}

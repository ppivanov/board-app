using System;
using System.Collections.Generic;

namespace BoardWebApp.Models
{
    public partial class Ticket
    {
        public Ticket()
        {
            Subtask = new HashSet<Subtask>();
        }

        public int TicketId { get; set; }
        public string TicketTitle { get; set; }
        public string TicketDescription { get; set; }
        public int TicketOrder { get; set; }
        public int ColumnId { get; set; }
        public int TicketTypeId { get; set; }
        public int? AssigneeId { get; set; }

        public virtual User Assignee { get; set; }
        public virtual BoardColumn Column { get; set; }
        public virtual TicketType TicketType { get; set; }
        public virtual ICollection<Subtask> Subtask { get; set; }
    }
}

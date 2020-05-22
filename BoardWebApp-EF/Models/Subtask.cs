using System;
using System.Collections.Generic;

namespace BoardWebApp.Models
{
    public partial class Subtask
    {
        public int SubtaskId { get; set; }
        public string SubtaskDescription { get; set; }
        public bool SubtaskDone { get; set; }
        public int SubtaskOrder { get; set; }
        public int TicketId { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}

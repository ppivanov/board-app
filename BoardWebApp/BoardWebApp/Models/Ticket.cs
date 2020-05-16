using System;
using System.Collections.Generic;

namespace BoardWebApp.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public string TicketTitle { get; set; }
        public string TicketDescription { get; set; }
        public int TicketOrder { get; set; }
        public BoardColumn Column { get; set; }
        public TicketType Type { get; set; }
        // public User  AssignedTo { get; set; }
        public List<SubTask> Subtasks { get; set; }

        public Ticket()
        {
        }
    }
}

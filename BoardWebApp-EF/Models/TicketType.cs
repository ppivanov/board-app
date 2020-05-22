using System;
using System.Collections.Generic;

namespace BoardWebApp.Models
{
    public partial class TicketType
    {
        public TicketType()
        {
            Ticket = new HashSet<Ticket>();
        }

        public int TicketTypeId { get; set; }
        public string TicketType1 { get; set; }

        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}

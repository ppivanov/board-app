using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardWebApp.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Board> ownerOfBoards { get; set; }
        public List<Project> ownerOfProject { get; set; }
        public List<Board> memberOfBoards { get; set; }
        public List<Project> memberOfProject { get; set; }
        public List<Ticket> assignedTickets { get; set; }

    }
}

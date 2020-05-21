using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardWebApp.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        //[NotMapped]
        //public List<Board> ownerOfBoards { get; set; }
        //[NotMapped]
        //public List<Project> ownerOfProject { get; set; }
        //[NotMapped]
        //public List<Board> memberOfBoards { get; set; }
        //[NotMapped]
        //public List<Project> memberOfProject { get; set; }
        //[NotMapped]
        //public List<Ticket> assignedTickets { get; set; }

        public User()
        {
            
        }

    }
}

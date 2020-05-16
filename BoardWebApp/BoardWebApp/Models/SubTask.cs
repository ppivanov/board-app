
namespace BoardWebApp.Models
{
    public class SubTask
    {
        public int SubtaskId { get; set; }
        public string SubtaskDescription { get; set; }
        public bool SubtaskDone { get; set; }
        public int SubtaskOrder { get; set; }
        public Ticket MyTicket { get; set; }

        public SubTask()
        {
        }
    }
}

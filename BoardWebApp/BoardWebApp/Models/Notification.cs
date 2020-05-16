using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardWebApp.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public string NotificationText { get; set; }
        public User ForUser { get; set; }
        public User FromUser { get; set; }
    }
}

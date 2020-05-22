using System;
using System.Collections.Generic;

namespace BoardWebApp.Models
{
    public partial class Notification
    {
        public int NotificationId { get; set; }
        public string NotificationText { get; set; }
        public int ForUserId { get; set; }
        public int FromUserId { get; set; }

        public virtual User ForUser { get; set; }
        public virtual User FromUser { get; set; }
    }
}

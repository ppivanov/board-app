using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardWebApp.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public User ProjectOwner { get; set; }
        public User ProjectScrumMaster { get; set; }
        public List<User> ProjectMembers { get; set; }
    }
}

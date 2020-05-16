﻿using System.Collections.Generic;

namespace BoardWebApp.Models
{
    public class Board
    {
        public int BoardId { get; set; }
        public string BoardName { get; set; }
        public string BoardDescription { get; set; }
        public User BoardOwner { get; set; }
        public List<BoardColumn> BoardColumns { get; set; }
        public BoardType Type { get; set; }
        public List<User> BoardMembers { get; set; }
        public Project ForProject { get; set; }

        public Board()
        {

        }
    }
}

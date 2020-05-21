using System.Collections.Generic;
using System.Linq;
using BoardAppDbContext.DB;
using BoardWebApp.Models;

namespace BoardWebApp.Repository
{
    public class UserRepository
    {
        public UserRepository()
        {
        }

        public List<User> getAllUsers()
        {
            using (var db = new BoardWepContext())
            {
                return db.User.ToList();
            }
        }
    }
}

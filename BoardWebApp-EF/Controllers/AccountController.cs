using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardWebApp.Models;
using BoardWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BoardWebApp.Controllers
{
    public class AccountController : Controller
    {
        private BoardWebAppContext _dbContext;
        public AccountController(BoardWebAppContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserRegistrationModel newUserValues)
        {
            if (EmailBelongsToAnotherUser(newUserValues.Email))
                Console.WriteLine("Email " + newUserValues.Email + " already in use");
            else
                Console.WriteLine("Email is all good!!");

            //TODO
            return View();
        }

        public bool EmailBelongsToAnotherUser(string value)
        {
            var EmailStore = _dbContext.User.Where(user => user.Email == value);
            var userFromQuery = EmailStore.FirstOrDefault();
            //If an email is already acossiated with another user 
            if (userFromQuery != null)
            {
                Console.WriteLine("userFromQuery's Email = " + userFromQuery.Email);
                return true;
            }
            // else the email is not related to any users and is free to use
            else
                return false;
        }
    }
}

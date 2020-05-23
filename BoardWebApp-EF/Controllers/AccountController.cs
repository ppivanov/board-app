using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BoardWebApp.Models;
using BoardWebApp.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BoardWebApp.Controllers
{
    public class AccountController : Controller
    {
        private BoardWebAppContext _dbContext;
        public AccountController(BoardWebAppContext dbContext)
        {
            _dbContext = dbContext;
            //Console.WriteLine(_dbContext.User.Where(user => user.Email == "notaracist@murica.com").FirstOrDefault().Email);
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(SendRegisterationModel registrationInformation)
        {
            UserRegistrationModel.UserRegistrationValidations(registrationInformation, _dbContext);
            if (UserRegistrationModel.PassedAllRegistrationValidations(registrationInformation.ValidationErrorMessages))
            {
                if (UserRegistrationModel.SaveUser(registrationInformation.userRegistrationModel, _dbContext))
                {
                    return RedirectToAction("Index", "Home");
                    //return RedirectToAction("Account", "Login");
                }
            }
            //TODO
            return View(registrationInformation);
        }

        //public bool EmailBelongsToAnotherUser(string value)
        //{
        //    var EmailStore = _dbContext.User.Where(user => user.Email == value);
        //    Console.WriteLine("AccountController dbContext # of results = " + EmailStore.ToArray().Length);
        //    var userFromQuery = EmailStore.FirstOrDefault();
        //    //If an email is already acossiated with another user 
        //    if (userFromQuery != null)
        //    {
        //        Console.WriteLine("userFromQuery's Email = " + userFromQuery.Email);
        //        return true;
        //    }
        //    // else the email is not related to any users and is free to use
        //    else
        //        return false;
        //}
        
    }
}



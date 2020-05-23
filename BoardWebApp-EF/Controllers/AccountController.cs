using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BoardWebApp.Models;
using BoardWebApp.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Net;

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

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(SendRegistrationModel registrationInformation)
        {
            string registrationSuccessful = "Thank you for registering! You can now log into your new account.";
            UserRegistrationModel.UserRegistrationValidationsStatic(registrationInformation, _dbContext);
            if (UserRegistrationModel.PassedAllRegistrationValidations(registrationInformation.ValidationErrorMessages))
            {
                if (UserRegistrationModel.SaveUser(registrationInformation.userRegistrationModel, _dbContext))
                {
                    return RedirectToAction("Index", "Home", new { @message = registrationSuccessful });
                    //return RedirectToAction("Account", "Login");
                }
            }
            return View(registrationInformation);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(SendLoginModel LoginInformation)
        {
            string LoginSuccessful = "Login successful!";
            if (UserLoginModel.LoginCredentialsMatchDatabaseRecord(LoginInformation, _dbContext))
            {
                string AuthenticationHashForCookie = UserLoginModel.CalculateHashForCookieForUserEmailAndDBContext(LoginInformation.userLoginModel.Email, _dbContext);
                Console.WriteLine("Login successful!");
        /****** COOKIE SETUP *******/
                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.MaxAge = new TimeSpan(1, 30, 0); // hours, minutes, seconds
                Response.Cookies.Append("BoardAppSessionCookie", AuthenticationHashForCookie, cookieOptions);
        /****** COOKIE SETUP *******/
                return RedirectToAction("Index", "Home", new { @message = LoginSuccessful });
            }
            else
            {
                Console.WriteLine("Log in failed!");
                return View(LoginInformation);
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            string LogoutSuccessFul = "Logout successful!";
            if (Request.Cookies["BoardAppSessionCookie"] != null)
            {
                Response.Cookies.Delete("BoardAppSessionCookie");
            }

            return RedirectToAction("Login", "Account", new { @message = LogoutSuccessFul });
        }

    }
}



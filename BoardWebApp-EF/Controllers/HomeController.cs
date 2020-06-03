using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BoardWebApp.Models;
using BoardWebApp.ViewModels;

namespace BoardWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private BoardWebAppContext _dbContext;

        public HomeController(ILogger<HomeController> logger, BoardWebAppContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

#nullable enable
        public IActionResult Index(HomePageModel homePageData)
        {
            if (homePageData.Message is null)
            {
                Console.WriteLine("homePageData is null at Home/Index");
                string cookieValue = Request.Cookies[AccountController.CookieId];
                User authenticatedUser = Models.User.AuthenticateBasedOnCookieValue(cookieValue, _dbContext);
                if(authenticatedUser == null)
                {
                    Console.WriteLine("User is NOT authenticated");
                    string errorMessageForLoginPage = "You must login before you can access the app!";
                    return RedirectToAction("Login", "Account", new { @errorMessage = errorMessageForLoginPage });
                }
                else
                {
                    Console.WriteLine("User IS authenticated");
                    homePageData = new HomePageModel(authenticatedUser, _dbContext);
                    return View(homePageData);
                }
            }
            else
            {
                Console.WriteLine("homePageData is NOT null at Home/Index");
                return View(homePageData);
            }
        }
#nullable disable

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Users()
        {
            List<User> allUsers = _dbContext.User.ToList();
            return View(allUsers);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

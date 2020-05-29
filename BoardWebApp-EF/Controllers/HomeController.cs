using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BoardWebApp.Models;

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
        public IActionResult Index(string? errorMessage)
        {
            if (errorMessage is null)
            {
                string cookieValue = Request.Cookies[AccountController.CookieId];
                User authenticatedUser = Models.User.AuthenticateBasedOnCookieValue(cookieValue, _dbContext);
                if(authenticatedUser == null)
                {
                    return View();
                }
                else
                {
                    return View("BoardWebApp");
                }
            }
            else
            {
                return View((object)errorMessage);
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

using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BoardWebApp.Models;
using BoardWebApp.Repository;

using BoardWebApp.DataBase;

namespace BoardWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private DBLogic DBConn { get; set; }
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            DBConn = new DBLogic();
        }

        public List<User> Index()
        {
            var usrObj = new UserRepository();
            return usrObj.getAllUsers();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BoardWebApp.Models;
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
            return DBConn.getAllUsers();
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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BoardWebApp.Models;
using BoardWebApp.DataBase;
using System.Data.SqlClient;
using System.IO.Pipelines;
using Newtonsoft.Json.Linq;

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

        public JObject Index()
        {
            //SqlDataReader reader = DBConn.getAllUsers();
            //string allUsers = "";
            //while (reader.Read())
            //{
            //    allUsers += reader;
            //}
            object res = DBConn.getAllUsers();
            string resString = res.ToString();
            resString = resString.Substring(1, (resString.Length - 2));
            Console.WriteLine(resString);
            return JObject.Parse(resString);
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

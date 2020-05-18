using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardWebApp.DataBase;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BoardWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var dbCob = DBConnection.Instance();
            dbCob.DatabaseName = "BoardWebApp";
            //try
            //{
            //    if (dbCob.IsConnect())
            //    {
            //        Console.WriteLine("Connection Success!");
            //        dbCob.Close();
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("Error: ->" + e.Message);

            //}
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
<<<<<<< HEAD

=======
using BoardWebApp.DataBase;
>>>>>>> 6b45137aa23d6c513470829ed86403afa659e5bd
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
<<<<<<< HEAD

=======
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
>>>>>>> 6b45137aa23d6c513470829ed86403afa659e5bd
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

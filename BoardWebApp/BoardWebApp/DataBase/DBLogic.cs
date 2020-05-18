using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BoardWebApp.Models;
using Dapper;

namespace BoardWebApp.DataBase
{
    public class DBLogic
    {
        private DBConnection DBConn { get; set; }

        //public DBLogic(DBConnection DBConn)
        //{
        //    this.DBConn = DBConn;
        //}

            public DBLogic()
        {
            DBConn = new DBConnection();
        }
        public List<User> getAllUsers() 
        {
            List<User> allUsers = new List<User>();
            string connstring = string.Format(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog={0};Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False", "BoardWebApp");
            string query = "Select * from [dbo].[User];";
            using (IDbConnection conn =new SqlConnection(connstring))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                allUsers = conn.Query<User>(query).ToList();
            }
            return allUsers;
        }
    }
}

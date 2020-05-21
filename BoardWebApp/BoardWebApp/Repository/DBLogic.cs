using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using BoardWebApp.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;

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
        public object getAllUsers() 
        {
            SqlDataReader allUsers;
            string connstring = string.Format(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog={0};Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False", "BoardWebApp");
            string query = "SELECT * FROM [dbo].[User] FOR JSON AUTO;";
            using (SqlConnection conn =new SqlConnection(connstring))
            {
                SqlCommand command = new SqlCommand(query, conn);
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                allUsers = command.ExecuteReader();
                allUsers.Read();
                return allUsers[0];
                //allUsers = conn.Query(query);
            }
            //return allUsers;
        }
        //public IEnumerable<Dictionary<string, object>> Serialize(SqlDataReader reader)
        //{
        //    var results = new List<Dictionary<string, object>>();
        //    var cols = new List<string>();
        //    for (var i = 0; i < reader.FieldCount; i++)
        //        cols.Add(reader.GetName(i));

        //    while (reader.Read())
        //        results.Add(SerializeRow(cols, reader));

        //    return results;
        //}
        //private Dictionary<string, object> SerializeRow(IEnumerable<string> cols,
        //                                                SqlDataReader reader)
        //{
        //    var result = new Dictionary<string, object>();
        //    foreach (var col in cols)
        //        result.Add(col, reader[col]);
        //    return result;
        //}
    }
}

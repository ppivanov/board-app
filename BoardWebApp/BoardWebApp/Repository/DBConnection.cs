using System;
using System.Data.SqlClient;

namespace BoardWebApp.DataBase
{
    public class DBConnection
    {
        public DBConnection()
        {
        }

        private string dbName = string.Empty;
        // Property
        public string DatabaseName
        {
            get
            {
                return dbName;
            }
            set
            {
                dbName = value;
            }
        }

        public string Password { get; set; }
        private SqlConnection connection = null;
        public SqlConnection Connection
        {
            get { return connection; } // returns a connection
        }


        private static DBConnection _instance = null;
        public static DBConnection Instance()
        {
            if (_instance == null)// Checking if there are open connections to DB
                _instance = new DBConnection();
            return _instance;// Returns a new instance connection to the DB
        }
        public bool IsConnect()
        {
            if (Connection == null)
            {
                if (String.IsNullOrEmpty(dbName))
                    return false;
                string connstring = string.Format("Server=localhost; Database={0};User Id=sa; Password=Password1234", dbName);
                connection = new SqlConnection(connstring);
                connection.Open();
            }

            return true;
        }

        public void Close()
        {
            connection.Close();
        }
    }
}

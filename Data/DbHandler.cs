using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace BloodBankManagementSystem.Data
{
    public class DbHandler
    {
        const string connectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = gep_demo; Integrated Security = True ";

        private SqlConnection conn = null;
        public SqlConnection OpenConnection()
        {
            conn = new SqlConnection(connectionString);
            conn.Open();
            return conn;

        }
        public bool CloseConnection()
        {
            bool isClosed = false;
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
                isClosed = true;
            }
            return isClosed;
        }
    }
}


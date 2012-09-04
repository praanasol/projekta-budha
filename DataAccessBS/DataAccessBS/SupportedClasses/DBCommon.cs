using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Configuration;


namespace DataAccessAutoRaksha.Supported_Classes
{
    public abstract class DBCommon
    {
        #region Private Variables

        private SqlConnection _SqlConnection;

        #endregion

        #region Public Properties

        public SqlConnection CSIConnection
        {
            get
            {
                if (_SqlConnection == null)
                {
                    _SqlConnection = GetConnection();
                }
                return _SqlConnection;
            }
        }

        public static string ConnectionString
        {
            get
            {
                return  ConfigurationManager.ConnectionStrings["bsConnectionString"].ToString();
            }
        }

        public static SqlConnection GetConnection()
        {
            SqlConnection myConn = new SqlConnection();
            // Get the connection string from the app.config file
            myConn.ConnectionString = ConfigurationManager.ConnectionStrings["bsConnectionString"].ToString();
            return myConn;
        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Configuration;
using System.Data.SqlClient;

namespace sdmcrmws.data
{
    public class DBCommon
    {
        public static string ConnString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["WsConnectionString"].ConnectionString;
            }
        }


        public static SqlDatabase dbConn = new SqlDatabase(DBCommon.ConnString);

    }
}

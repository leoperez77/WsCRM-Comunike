using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
namespace sdmcrmws.data
{
    public class DBHelper
    {
        public static SqlDataReader GetDataReader(string Sql)
        {
            DbCommand cmd = DBCommon.dbConn.GetSqlStringCommand(Sql);
            return (SqlDataReader) DBCommon.dbConn.ExecuteReader(cmd);
        }

        public static DataSet GetDataSet(string Sql)
        {
            DbCommand cmd = DBCommon.dbConn.GetSqlStringCommand(Sql.Replace("--",""));            
            return DBCommon.dbConn.ExecuteDataSet(cmd);
        }

        public static int Execute(string Sql)
        {
            DbCommand cmd = DBCommon.dbConn.GetSqlStringCommand(Sql);
            cmd.CommandType = System.Data.CommandType.Text;
            return DBCommon.dbConn.ExecuteNonQuery(cmd);           
        }

        public static int ExecSp(string Sp, string[] Params)
        {                    
            return DBCommon.dbConn.ExecuteNonQuery(Sp, Params);
        }

       
    }
}

using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using smdcrmws.dto;
namespace sdmcrmws.data
{
    public class DBUsuario
    {
        
        public static List<wsOperario> GetOperarios(string IdEmpresa)
        {

            List<wsOperario> results = new List<wsOperario>();
            DbCommand cmd = DBCommon.dbConn.GetStoredProcCommand("CMGetoperarios");
            DBCommon.dbConn.AddInParameter(cmd, "@id_emp", DbType.Int16, int.Parse(IdEmpresa));

            //((RefCountingDataReader)db.ExecuteReader(command)).InnerReader as SqlDataReader;
            using (IDataReader dr = DBCommon.dbConn.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    wsOperario obj = new wsOperario();
                    int iCampo = 1;
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        obj["Campo_" + iCampo.ToString()] = !dr.IsDBNull(i) ? dr.GetString(i) : "";
                        iCampo++;
                    }

                    results.Add(obj);

                }
                dr.Close();
            }

            return results;
        }
    }
}

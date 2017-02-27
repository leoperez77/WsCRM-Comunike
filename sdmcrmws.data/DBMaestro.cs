using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using smdcrmws.dto;
namespace sdmcrmws.data
{
    public class DBMaestro
    {
        public static List<wsMaestro> GetMaestro(string IdEmpresa, string SpLectura)
        {

            List<wsMaestro> results = new List<wsMaestro>();
            DbCommand cmd = DBCommon.dbConn.GetStoredProcCommand(SpLectura);
            DBCommon.dbConn.AddInParameter(cmd, "@id_emp", DbType.Int16, int.Parse(IdEmpresa));

            //((RefCountingDataReader)db.ExecuteReader(command)).InnerReader as SqlDataReader;
            using (IDataReader dr = DBCommon.dbConn.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    wsMaestro obj = new wsMaestro();
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

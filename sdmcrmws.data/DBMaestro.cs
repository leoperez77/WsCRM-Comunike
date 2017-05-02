using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using smdcrmws.dto;
using System;

namespace sdmcrmws.data
{
    public class DBMaestro
    {
        public static List<wsMaestro> GetMaestro(int IdEmpresa, string SpLectura)
        {

            List<wsMaestro> results = new List<wsMaestro>();
            DbCommand cmd = DBCommon.dbConn.GetStoredProcCommand(SpLectura);
            DBCommon.dbConn.AddInParameter(cmd, "@id_emp", DbType.Int16, IdEmpresa);

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

        public static wsControl MarcarMaestroSincronizado(int IdMaestro, string SpMarcacion)
        {
            wsControl obj = new wsControl();
            obj.FechaHora = DateTime.Now.ToString();
            obj.Origen = System.Reflection.MethodBase.GetCurrentMethod().Name;

            try
            {

                DbCommand cmd = DBCommon.dbConn.GetStoredProcCommand(SpMarcacion);
                DBCommon.dbConn.AddInParameter(cmd, "@id", DbType.Int32, IdMaestro);

                try
                {
                    DBCommon.dbConn.ExecuteNonQuery(cmd);
                }
                catch
                {
                    throw;
                }

                obj.Estado = "ok";
            }
            catch (Exception ex)
            {
                obj.Estado = "error";
                obj.Error = ex.Message;
                if (ex.InnerException != null)
                {
                    obj.Error += Environment.NewLine + ex.InnerException.Message;
                }
            }

            return obj;
        }
    }
}

using System;
using System.Collections.Generic;
using smdcrmws.dto;
using System.Data;
using System.Data.Common;
using System.IO;
using Newtonsoft.Json;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace sdmcrmws.data
{
    public static class DBCita
    {
        public static wsControl PutCita(Stream JSONdataStream)
        {
            wsControl obj = new wsControl();
            obj.FechaHora = DateTime.Now.ToString();
            obj.Origen = System.Reflection.MethodBase.GetCurrentMethod().Name;

            SqlConnection Conn = (SqlConnection)DBCommon.dbConn.CreateConnection();
            Conn.Open();
            SqlTransaction Tr = Conn.BeginTransaction();

            try
            {
                // Read in our Stream into a string...
                StreamReader reader = new StreamReader(JSONdataStream);
                string JSONdata = reader.ReadToEnd();

                var Cita = JsonConvert.DeserializeObject<wsCita>(JSONdata);

                if (Cita == null)
                {                       
                    throw new System.InvalidOperationException("Objeto JSON no pudo convertirse en cita");
                }

                int IdRetorno = 0;
                DbCommand cmd = DBCommon.dbConn.GetStoredProcCommand("PutTalCitas");
                DBCommon.dbConn.AddInParameter(cmd, "@emp", DbType.Int32, int.Parse(Cita.IdEmpresa));
                DBCommon.dbConn.AddInParameter(cmd, "@id", DbType.Int32, int.Parse(Cita.Id));
                DBCommon.dbConn.AddInParameter(cmd, "@bod", DbType.Int32, int.Parse(Cita.Bodega));
                DBCommon.dbConn.AddInParameter(cmd, "@placa", DbType.Int32, int.Parse(Cita.IdVehiculo));
                DBCommon.dbConn.AddInParameter(cmd, "@plan", DbType.Int32, int.Parse(Cita.IdPlan));
                DBCommon.dbConn.AddInParameter(cmd, "@camp", DbType.Int32, int.Parse(Cita.IdCamp));
                DBCommon.dbConn.AddInParameter(cmd, "@hora", DbType.DateTime, DateTime.Parse(Cita.Hora));
                DBCommon.dbConn.AddInParameter(cmd, "@nom", DbType.String, Cita.Responsable);
                DBCommon.dbConn.AddInParameter(cmd, "@tel", DbType.String, Cita.Telefono);
                DBCommon.dbConn.AddInParameter(cmd, "@notas", DbType.String, Cita.Notas);                     
                DBCommon.dbConn.ExecuteNonQuery(cmd, Tr);
           
                Tr.Commit();

                obj.IdGenerado = "0";
                obj.Estado = "ok";
            }
            catch (Exception ex)
            {
                Tr.Rollback();
                obj.Estado = "error";
                obj.Error = ex.Message;
                if (ex.InnerException != null)
                {
                    obj.Error += Environment.NewLine + ex.InnerException.Message;
                }
            }
            finally
            {
                Conn.Close();
            }

            return obj;

        }

        public static List<wsMaestro> GetCitasDia(int IdEmpresa, int IdBodega, DateTime Fecha)
        {
            List<wsMaestro> results = new List<wsMaestro>();
            DbCommand cmd = DBCommon.dbConn.GetStoredProcCommand("CMGet_tal_citas_fecha");
            DBCommon.dbConn.AddInParameter(cmd, "@emp", DbType.Int32, IdEmpresa);
            DBCommon.dbConn.AddInParameter(cmd, "@bod", DbType.Int32, IdBodega);
            DBCommon.dbConn.AddInParameter(cmd, "@fecha", DbType.Date, Fecha);

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

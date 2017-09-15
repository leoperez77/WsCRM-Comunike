using System;
using System.Collections.Generic;
using smdcrmws.dto;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Web.Script.Serialization;
namespace sdmcrmws.data
{
    public class DBVehiculos
    {
        public static List<wsVehiculo> GetVehiculos(string IdEmpresa)
        {
            List<wsVehiculo> results = new List<wsVehiculo>();

            DbCommand cmd = DBCommon.dbConn.GetStoredProcCommand("CMGetcot_item_lotes");
            DBCommon.dbConn.AddInParameter(cmd, "@id_emp", DbType.Int16, int.Parse(IdEmpresa));

            //((RefCountingDataReader)db.ExecuteReader(command)).InnerReader as SqlDataReader;
            using (IDataReader dr = DBCommon.dbConn.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    wsVehiculo obj = new wsVehiculo();
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

        public static List<wsLineaModelo> GetModelosLinea(string IdEmpresa)
        {
            List<wsLineaModelo> results = new List<wsLineaModelo>();

            DbCommand cmd = DBCommon.dbConn.GetStoredProcCommand("CMGetveh_linea_modelos");
            DBCommon.dbConn.AddInParameter(cmd, "@id_emp", DbType.Int16, int.Parse(IdEmpresa));

            //((RefCountingDataReader)db.ExecuteReader(command)).InnerReader as SqlDataReader;
            using (IDataReader dr = DBCommon.dbConn.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    wsLineaModelo obj = new wsLineaModelo();
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

        public static wsControl MarcarModeloLineaSincronizado(int IdItem)
        {
            wsControl obj = new wsControl();
            obj.FechaHora = DateTime.Now.ToString();
            obj.Origen = System.Reflection.MethodBase.GetCurrentMethod().Name;

            try
            {

                DbCommand cmd = DBCommon.dbConn.GetStoredProcCommand("CMSyncveh_linea_modelo");
                DBCommon.dbConn.AddInParameter(cmd, "@id", DbType.Int32, IdItem);

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

        public static wsControl MarcarVehiculoSincronizado(int IdVehiculo)
        {
            wsControl obj = new wsControl();
            obj.FechaHora = DateTime.Now.ToString();
            obj.Origen = System.Reflection.MethodBase.GetCurrentMethod().Name;

            try
            {

                DbCommand cmd = DBCommon.dbConn.GetStoredProcCommand("CMSynccot_item_lote");
                DBCommon.dbConn.AddInParameter(cmd, "@id", DbType.Int32, IdVehiculo);

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

        public static List<wsVehiculo> GetStockVehiculos(string IdEmpresa, string IdBodega)
        {
            List<wsVehiculo> results = new List<wsVehiculo>();

            DbCommand cmd = DBCommon.dbConn.GetStoredProcCommand("Get2540");
            DBCommon.dbConn.AddInParameter(cmd, "@emp", DbType.Int16, int.Parse(IdEmpresa));
            DBCommon.dbConn.AddInParameter(cmd, "@bod", DbType.Int16, int.Parse(IdBodega));
            DBCommon.dbConn.AddInParameter(cmd, "@prov", DbType.Int16, 0);
            DBCommon.dbConn.AddInParameter(cmd, "@item", DbType.Int16, 0);
            DBCommon.dbConn.AddInParameter(cmd, "@solo_stock", DbType.Int16, 1);


            //((RefCountingDataReader)db.ExecuteReader(command)).InnerReader as SqlDataReader;
            using (IDataReader dr = DBCommon.dbConn.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    wsVehiculo obj = new wsVehiculo();
                    int iCampo = 1;
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        obj["Campo_" + iCampo.ToString()] = !dr.IsDBNull(i) ? dr[i].ToString() : "";
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

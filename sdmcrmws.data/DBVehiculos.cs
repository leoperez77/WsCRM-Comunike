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
        public static wsControl PutVehiculo(Stream JSONdataStream)
        {
            wsControl obj = new wsControl();
            obj.FechaHora = DateTime.Now.ToString();
            obj.Origen = System.Reflection.MethodBase.GetCurrentMethod().Name;

            try
            {
                // Read in our Stream into a string...
                StreamReader reader = new StreamReader(JSONdataStream);
                string JSONdata = reader.ReadToEnd();

                JavaScriptSerializer jss = new JavaScriptSerializer();
                wsVehiculo vehiculo = jss.Deserialize<wsVehiculo>(JSONdata);
                if (vehiculo == null)
                {
                    throw new System.InvalidOperationException("Objeto JSON no pudo convertirse en objeto wsvehiculo");
                }

                DbCommand cmd = DBCommon.dbConn.GetStoredProcCommand("PutVehCrearVehiculo");
                DBCommon.dbConn.AddInParameter(cmd, "@emp", DbType.Int32, int.Parse(vehiculo.Campo_21));
                DBCommon.dbConn.AddInParameter(cmd, "@usu", DbType.Int32, Helper.Bcero(vehiculo.Campo_22));//
                DBCommon.dbConn.AddInParameter(cmd, "@id", DbType.Int32, 0);//
                DBCommon.dbConn.AddInParameter(cmd, "@id_cot_item", DbType.Int32, int.Parse(vehiculo.Campo_2));
                DBCommon.dbConn.AddInParameter(cmd, "@lote", DbType.String, vehiculo.Campo_3);//
                DBCommon.dbConn.AddInParameter(cmd, "@fvcmto", DbType.Date ,vehiculo.Campo_5);
                DBCommon.dbConn.AddInParameter(cmd, "@notas", DbType.String, vehiculo.Campo_6);//
                DBCommon.dbConn.AddInParameter(cmd, "@motor", DbType.String, vehiculo.Campo_10);//
                DBCommon.dbConn.AddInParameter(cmd, "@placa", DbType.String, vehiculo.Campo_11);//
                DBCommon.dbConn.AddInParameter(cmd, "@chasis", DbType.String, vehiculo.Campo_12);//
                DBCommon.dbConn.AddInParameter(cmd, "@km", DbType.Int32, Helper.Bcero(vehiculo.Campo_13));//
                DBCommon.dbConn.AddInParameter(cmd, "@id_veh_color_int", DbType.Int32, Helper.Bcero(vehiculo.Campo_14));//
                DBCommon.dbConn.AddInParameter(cmd, "@id_veh_color", DbType.Int32, Helper.Bcero(vehiculo.Campo_15));//                
                DBCommon.dbConn.AddInParameter(cmd, "@licencia_transito", DbType.String, vehiculo.Campo_17);
                DBCommon.dbConn.AddInParameter(cmd, "@seguro_obligatorio", DbType.String, vehiculo.Campo_18);                
                DBCommon.dbConn.AddInParameter(cmd, "@id_cot_cliente_aseguradora", DbType.Int32, Helper.Bcero(vehiculo.Campo_20));//
                DBCommon.dbConn.AddInParameter(cmd, "@id_cot_cliente_contacto", DbType.Int32, Helper.Bcero(vehiculo.Campo_16));//
                DBCommon.dbConn.AddInParameter(cmd, "@id_cot_cliente_pais", DbType.Int32, Helper.Bcero(vehiculo.Campo_19));//

                try
                {
                    DBCommon.dbConn.ExecuteNonQuery(cmd);

                    if (int.Parse(vehiculo.Campo_1) == 0)
                    {
                        DbCommand cmdcl = DBCommon.dbConn.GetStoredProcCommand("GetCotItemLotesCRM");
                        DBCommon.dbConn.AddInParameter(cmdcl, "@id", DbType.Int32, int.Parse(vehiculo.Campo_2));
                        DBCommon.dbConn.AddInParameter(cmdcl, "@bod ", DbType.Int32, 0);
                        DBCommon.dbConn.AddInParameter(cmdcl, "@constock", DbType.Int32, 0);

                        DataSet dsc = DBCommon.dbConn.ExecuteDataSet(cmdcl);
                        obj.IdGenerado = int.Parse(dsc.Tables[0].Rows[0]["id"].ToString()).ToString();
                    }
                }
                catch(Exception ex)
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

        public static List<wsMaestro> GetEstadistica(int IdVehiculo, DateTime FechaDesde, DateTime FechaHasta)
        {
            List<wsMaestro> results = new List<wsMaestro>();
            DbCommand cmd = DBCommon.dbConn.GetStoredProcCommand("CMGet_estadistica_vehiculo");
            DBCommon.dbConn.AddInParameter(cmd, "@id_cot_item_lote", DbType.Int32, IdVehiculo);
            DBCommon.dbConn.AddInParameter(cmd, "@fecinf", DbType.Date, FechaDesde);
            DBCommon.dbConn.AddInParameter(cmd, "@fecsup", DbType.Date, FechaHasta);

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


        public static List<wsMaestro> GetStatusHn(int Id)
        {
            List<wsMaestro> results = new List<wsMaestro>();
            DbCommand cmd = DBCommon.dbConn.GetStoredProcCommand("CMGet_statusHn");
            DBCommon.dbConn.AddInParameter(cmd, "@id", DbType.Int32, Id);
           
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

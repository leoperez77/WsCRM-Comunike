using System;
using System.Collections.Generic;
using smdcrmws.dto;
using System.Data;
using System.Data.Common;
using System.IO;
using Newtonsoft.Json;
namespace sdmcrmws.data
{
    public class DBItem
    {
        public static List<wsItem> GetItemsSincronizar(string IdEmpresa, int Vehiculos)
        {
            List<wsItem> results = new List<wsItem>();

            DbCommand cmd = DBCommon.dbConn.GetStoredProcCommand("CMGetcot_items");
            DBCommon.dbConn.AddInParameter(cmd, "@id_emp", DbType.Int16, int.Parse(IdEmpresa));
            DBCommon.dbConn.AddInParameter(cmd, "@vehiculos", DbType.Int16, Vehiculos);

            //((RefCountingDataReader)db.ExecuteReader(command)).InnerReader as SqlDataReader;
            using (IDataReader dr = DBCommon.dbConn.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    wsItem obj = new wsItem();
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

        public static wsControl PutItemsSincronizar(Stream JSONdataStream)
        {
            wsControl obj = new wsControl();
            obj.FechaHora = DateTime.Now.ToString();
            obj.Origen = System.Reflection.MethodBase.GetCurrentMethod().Name;

            try
            {
                // Read in our Stream into a string...
                StreamReader reader = new StreamReader(JSONdataStream);
                string JSONdata = reader.ReadToEnd();
                
                //JavaScriptSerializer jss = new JavaScriptSerializer();
                //var Items = jss.Deserialize<List<wsItem>>(JSONdata);

                var Items = JsonConvert.DeserializeObject <List<wsItem>>(JSONdata);

                if (Items == null)
                {
                    // Error: Couldn't deserialize our JSON string into a "wsOrder" object.
                    throw new System.InvalidOperationException("Objeto JSON no pudo convertirse en arreglo de objetos wsItem");
                }

                for (int i = 0; i < Items.Count; i++)
                {
                    wsItem Item = Items[i];

                    DbCommand cmd = DBCommon.dbConn.GetStoredProcCommand("CMSynccot_items");
                    DBCommon.dbConn.AddInParameter(cmd, "@id", DbType.Int32, int.Parse(Item.Campo_2));

                    try
                    {
                        DBCommon.dbConn.ExecuteNonQuery(cmd);
                    }
                    catch
                    {
                        obj.Error = "Linea " + (i + 1).ToString() + Environment.NewLine;
                        throw;
                    }
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

        public static wsControl MarcarItemSincronizado(int IdItem)
        {
            wsControl obj = new wsControl();
            obj.FechaHora = DateTime.Now.ToString();
            obj.Origen = System.Reflection.MethodBase.GetCurrentMethod().Name;

            try
            {                

                DbCommand cmd = DBCommon.dbConn.GetStoredProcCommand("CMSynccot_items");
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

        public static wsStock GetStock(int IdItem, int IdBodega)
        {
            var obj = new wsStock();

            DbCommand cmd = DBCommon.dbConn.GetStoredProcCommand("CMGetstock");
            DBCommon.dbConn.AddInParameter(cmd, "@idItem", DbType.Int32, IdItem);
            DBCommon.dbConn.AddInParameter(cmd, "@IdBodega", DbType.Int32, IdBodega);

            try
            {
                DataSet ds = DBCommon.dbConn.ExecuteDataSet(cmd);
                DataTable dtr = ds.Tables[0];
                DataTable dtd = ds.Tables[1];

                obj.IdItem = IdItem.ToString();

                if (dtr.Rows.Count > 0)
                {
                    DataRow dr = dtr.Rows[0];
                    obj.Pendiente = dr["pendiente"].ToString();
                    obj.Stock = dr["stock"].ToString();
                    obj.Und = dr["und"].ToString();
                    obj.CanUnd = dr["und_cant"].ToString();
                }

                List<wsDetalleStock> ldet = new List<wsDetalleStock>();

                foreach (DataRow drd in dtd.Rows)
                {
                    var det = new wsDetalleStock();
                    det.IdLote = drd["id_cot_item_lote"].ToString();
                    det.Lote = drd["lote"].ToString();
                    det.Pendiente = drd["pendiente"].ToString();
                    det.Stock = drd["stock"].ToString();
                    det.Transito = drd["transito"].ToString();
                    if(drd["fecha_vencimiento"].ToString()!="")
                        det.Vencimiento = DateTime.Parse(drd["fecha_vencimiento"].ToString()).ToString("yyyy-MM-dd");
                    ldet.Add(det);
                }
                obj.Detalle = ldet;
            }
            catch
            {
                throw;
            }

            return obj;
        }

        public static wsControl PutDetallePlanSincronizar(string IdDetalle)
        {
            wsControl obj = new wsControl();
            obj.FechaHora = DateTime.Now.ToString();
            obj.Origen = System.Reflection.MethodBase.GetCurrentMethod().Name;

            try
            {
               
               
                DbCommand cmd = DBCommon.dbConn.GetStoredProcCommand("CMSynccot_item_prereq");
                DBCommon.dbConn.AddInParameter(cmd, "@id", DbType.Int32, IdDetalle);

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

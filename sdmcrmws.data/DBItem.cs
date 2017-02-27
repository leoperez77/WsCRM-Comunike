using System;
using System.Collections.Generic;
using smdcrmws.dto;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Web.Script.Serialization;
namespace sdmcrmws.data
{
    public class DBItem
    {
        public static List<wsItem> GetItemsSincronizar(string IdEmpresa)
        {
            List<wsItem> results = new List<wsItem>();

            DbCommand cmd = DBCommon.dbConn.GetStoredProcCommand("CMGetcot_items");
            DBCommon.dbConn.AddInParameter(cmd, "@id_emp", DbType.Int16, int.Parse(IdEmpresa));

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

                JavaScriptSerializer jss = new JavaScriptSerializer();
                var Items = jss.Deserialize<List<wsItem>>(JSONdata);

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
    }
}

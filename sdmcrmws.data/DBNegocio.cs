using System;
using System.Collections.Generic;
using smdcrmws.dto;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
namespace sdmcrmws.data
{
    public class DBNegocio
    {
        public static wsControl PutEncabezadoNegocio(Stream JSONdataStream)
        {
            wsControl obj = new wsControl();
            obj.FechaHora = DateTime.Now.ToString();
            obj.Origen = System.Reflection.MethodBase.GetCurrentMethod().Name;

            try
            {
                // Read in our Stream into a string...
                StreamReader reader = new StreamReader(JSONdataStream);
                string JSONdata = reader.ReadToEnd();
                               
                var Negocio = JsonConvert.DeserializeObject<wsNegocio>(JSONdata);

                if (Negocio == null)
                {
                    // Error: Couldn't deserialize our JSON string into a "wsOrder" object.
                    throw new System.InvalidOperationException("Objeto JSON no pudo convertirse hoja de negocio");
                }

                int IdRetorno = 0;
                DbCommand cmd = DBCommon.dbConn.GetStoredProcCommand("PutVehHnEnca");
                DBCommon.dbConn.AddOutParameter(cmd, "@idRetorno", DbType.Int32, IdRetorno);
                DBCommon.dbConn.AddInParameter(cmd, "@id_emp", DbType.Int32, int.Parse(Negocio.IdEmpresa));
                DBCommon.dbConn.AddInParameter(cmd, "@id", DbType.Int32, int.Parse(Negocio.Id));
                DBCommon.dbConn.AddInParameter(cmd, "@fecha", DbType.DateTime, DateTime.Parse(Negocio.Fecha));
                DBCommon.dbConn.AddInParameter(cmd, "@estado", DbType.Int32, int.Parse(Negocio.Estado));
                DBCommon.dbConn.AddInParameter(cmd, "@id_cot_tipo", DbType.Int32, int.Parse(Negocio.TipoDocumento));
                DBCommon.dbConn.AddInParameter(cmd, "@id_cot_bodega", DbType.Int32, int.Parse(Negocio.Bodega));
                DBCommon.dbConn.AddInParameter(cmd, "@id_cot_cliente_contacto", DbType.Int32, int.Parse(Negocio.Contacto));
                DBCommon.dbConn.AddInParameter(cmd, "@id_usuario", DbType.Int32, int.Parse(Negocio.Usuario));
                DBCommon.dbConn.AddInParameter(cmd, "@id_usuario_vende", DbType.Int32, int.Parse(Negocio.Vendedor));
                DBCommon.dbConn.AddInParameter(cmd, "@id_usuario_rev", DbType.Int32, int.Parse(Negocio.UsuarioRevisa));
                DBCommon.dbConn.AddInParameter(cmd, "@id_usuario_aprobo", DbType.Int32, int.Parse(Negocio.UsuarioAprueba));
                DBCommon.dbConn.AddInParameter(cmd, "@fechaentrega", DbType.DateTime, DateTime.Parse(Negocio.FechaEntrega));
                DBCommon.dbConn.AddInParameter(cmd, "@doc_ref", DbType.String, Negocio.DocReferencia);
                DBCommon.dbConn.AddInParameter(cmd, "@notas", DbType.String, Negocio.Notas);
                DBCommon.dbConn.AddInParameter(cmd, "@notas_reviso", DbType.String, Negocio.NotasRevision);
                DBCommon.dbConn.AddInParameter(cmd, "@notas_aprobo", DbType.String, Negocio.NotasAprobacion);
                
                try
                {
                    DBCommon.dbConn.ExecuteNonQuery(cmd);
                    obj.IdGenerado = DBCommon.dbConn.GetParameterValue(cmd, "@idRetorno").ToString();
                    //db.GetParameterValue(dbCommand, "@ProductID"),
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

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
                DBCommon.dbConn.AddOutParameter(cmd,"@idRetorno", DbType.Int32, IdRetorno);
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

        public static wsControl PutCotizacion(Stream JSONdataStream)
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

                var Cotizacion = JsonConvert.DeserializeObject<wsCotizacion>(JSONdata);

                if (Cotizacion == null)
                {
                    // Error: Couldn't deserialize our JSON string into a "wsOrder" object.
                    throw new System.InvalidOperationException("Objeto JSON no pudo convertirse en cotización");
                }
                   
                int IdRetorno = 0;
                DbCommand cmd = DBCommon.dbConn.GetStoredProcCommand("PutCotCotiza4");
                DBCommon.dbConn.AddOutParameter(cmd,"@idRetorno", DbType.Int32, IdRetorno);
                DBCommon.dbConn.AddInParameter(cmd, "@emp", DbType.Int32, int.Parse(Cotizacion.IdEmpresa));
                DBCommon.dbConn.AddInParameter(cmd, "@usucrea", DbType.Int32, int.Parse(Cotizacion.Usuario));
                DBCommon.dbConn.AddInParameter(cmd, "@bod", DbType.Int32, int.Parse(Cotizacion.Bodega));
                DBCommon.dbConn.AddInParameter(cmd, "@usu", DbType.Int32, int.Parse(Cotizacion.Usuario)*-1);
                DBCommon.dbConn.AddInParameter(cmd, "@id", DbType.Int32, int.Parse(Cotizacion.Id));
                DBCommon.dbConn.AddInParameter(cmd, "@fecha", DbType.DateTime, DateTime.Parse(Cotizacion.Fecha));
                DBCommon.dbConn.AddInParameter(cmd, "@idtipo", DbType.Int32, int.Parse(Cotizacion.TipoDocumento));
                DBCommon.dbConn.AddInParameter(cmd, "@idcli", DbType.Int32, int.Parse(Cotizacion.Cliente));
                DBCommon.dbConn.AddInParameter(cmd, "@idvend", DbType.Int32, int.Parse(Cotizacion.Vendedor));
                DBCommon.dbConn.AddInParameter(cmd, "@idformapago", DbType.Int32, int.Parse(Cotizacion.FormaPago));
                DBCommon.dbConn.AddInParameter(cmd, "@idcontacto", DbType.Int32, int.Parse(Cotizacion.Contacto));
                DBCommon.dbConn.AddInParameter(cmd, "@dias", DbType.Int32, int.Parse(Cotizacion.Dias));
                DBCommon.dbConn.AddInParameter(cmd, "@tot_Sub", DbType.Currency, decimal.Parse(Cotizacion.Subtotal));
                DBCommon.dbConn.AddInParameter(cmd, "@tot_Descuento", DbType.Currency, decimal.Parse(Cotizacion.Descuento));
                DBCommon.dbConn.AddInParameter(cmd, "@tot_Iva", DbType.Currency, decimal.Parse(Cotizacion.Iva));
                DBCommon.dbConn.AddInParameter(cmd, "@tot_Tot", DbType.Currency, decimal.Parse(Cotizacion.Subtotal));
                DBCommon.dbConn.AddInParameter(cmd, "@idestado", DbType.Int32, int.Parse(Cotizacion.Estado));
                DBCommon.dbConn.AddInParameter(cmd, "@moneda", DbType.Int32, int.Parse(Cotizacion.Moneda));
                DBCommon.dbConn.AddInParameter(cmd, "@Tasa", DbType.Currency, decimal.Parse(Cotizacion.Tasa));
                DBCommon.dbConn.AddInParameter(cmd, "@fechaestimada", DbType.DateTime, DateTime.Parse(Cotizacion.FechaEstimada));
                DBCommon.dbConn.AddInParameter(cmd, "@facti", DbType.Int32, int.Parse(Cotizacion.Factibilidad));
                DBCommon.dbConn.AddInParameter(cmd, "@notasinternas", DbType.String, Cotizacion.NotasInternas);
                DBCommon.dbConn.AddInParameter(cmd, "@notas", DbType.String, Cotizacion.Notas);
                DBCommon.dbConn.AddInParameter(cmd, "@final", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@nada", DbType.String, "");
                DBCommon.dbConn.AddInParameter(cmd, "@km", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@IdItemLote", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@debe", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@ret1", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@ret2", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@ret3", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@ret4", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@ret5", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@ret6", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@rombo", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@docref_tipo", DbType.String, "");
                DBCommon.dbConn.AddInParameter(cmd, "@docref_numero", DbType.String, "");
                DBCommon.dbConn.AddInParameter(cmd, "@ajuste", DbType.Currency, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@id_ant", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@idcli2", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@deducible", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@deducibleminimo", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@idproyecto", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@haytaller", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@id_med", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@id_ent", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@bod_dest", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@motivo", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@r1", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@r2", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@r3", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@r4", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@r5", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@r6", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@fechacartera", DbType.DateTime, DateTime.Parse(Cotizacion.Fecha));
                DBCommon.dbConn.AddInParameter(cmd, "@fechadesconectada", DbType.DateTime, DateTime.Parse(Cotizacion.Fecha));
                DBCommon.dbConn.AddInParameter(cmd, "@PrecioNro", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@NuevoDia", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@Id_Veh_hn_enc", DbType.Int32, Cotizacion.Negocio);
                DBCommon.dbConn.AddInParameter(cmd, "@id_orden_taller", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@id_pedido_taller", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@tal_ope", DbType.String, "");
                DBCommon.dbConn.AddInParameter(cmd, "@tal_def", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@tot_Iva2", DbType.Currency, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@hacer_rc", DbType.Int32, 0);
                               
                DBCommon.dbConn.ExecuteNonQuery(cmd);
                Tr.Commit();

                IdRetorno = int.Parse(DBCommon.dbConn.GetParameterValue(cmd, "@idRetorno").ToString());
                obj.IdGenerado = IdRetorno.ToString();

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
    }
}

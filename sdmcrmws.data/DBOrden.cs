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
    public static class DBOrden
    {
        public static wsControl PutOrden(Stream JSONdataStream)
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

                var Cotizacion = JsonConvert.DeserializeObject<wsOrdenTaller>(JSONdata);

                if (Cotizacion == null)
                {
                    // Error: Couldn't deserialize our JSON string into a "wsOrder" object.
                    throw new System.InvalidOperationException("Objeto JSON no pudo convertirse en encabezado orden");
                }
                
                int IdRetorno = 0;
                DbCommand cmd = DBCommon.dbConn.GetStoredProcCommand("PutTalOrden");
                DBCommon.dbConn.AddOutParameter(cmd, "@idRetorno", DbType.Int32, IdRetorno);
                DBCommon.dbConn.AddInParameter(cmd, "@emp", DbType.Int32, int.Parse(Cotizacion.IdEmpresa));
                DBCommon.dbConn.AddInParameter(cmd, "@usucrea", DbType.Int32, int.Parse(Cotizacion.Usuario));
                DBCommon.dbConn.AddInParameter(cmd, "@bod", DbType.Int32, int.Parse(Cotizacion.Bodega));
                DBCommon.dbConn.AddInParameter(cmd, "@usu", DbType.Int32, int.Parse(Cotizacion.Usuario) * -1);
                DBCommon.dbConn.AddInParameter(cmd, "@id", DbType.Int32, int.Parse(Cotizacion.Id));
                DBCommon.dbConn.AddInParameter(cmd, "@fecha", DbType.DateTime, DateTime.Parse(Cotizacion.Fecha));
                DBCommon.dbConn.AddInParameter(cmd, "@idtipo", DbType.Int32, int.Parse(Cotizacion.TipoDocumento));
                DBCommon.dbConn.AddInParameter(cmd, "@idcli", DbType.Int32, int.Parse(Cotizacion.Cliente));
                DBCommon.dbConn.AddInParameter(cmd, "@idvend", DbType.Int32, int.Parse(Cotizacion.Vendedor));
                DBCommon.dbConn.AddInParameter(cmd, "@idcontacto", DbType.Int32, int.Parse(Cotizacion.Contacto));
                DBCommon.dbConn.AddInParameter(cmd, "@tot_Sub", DbType.Currency, decimal.Parse(Cotizacion.Subtotal));
                DBCommon.dbConn.AddInParameter(cmd, "@tot_Descuento", DbType.Currency, decimal.Parse(Cotizacion.Descuento));
                DBCommon.dbConn.AddInParameter(cmd, "@tot_Iva", DbType.Currency, decimal.Parse(Cotizacion.Iva));
                DBCommon.dbConn.AddInParameter(cmd, "@tot_Tot", DbType.Currency, decimal.Parse(Cotizacion.Subtotal));
                DBCommon.dbConn.AddInParameter(cmd, "@fechaestimada", DbType.DateTime, DateTime.Parse(Cotizacion.FechaEstimada));
                DBCommon.dbConn.AddInParameter(cmd, "@notasinternas", DbType.String, Cotizacion.NotasInternas);
                DBCommon.dbConn.AddInParameter(cmd, "@notas", DbType.String, Cotizacion.Notas);
                DBCommon.dbConn.AddInParameter(cmd, "@Km", DbType.Int32, int.Parse(Cotizacion.Kilometraje));
                DBCommon.dbConn.AddInParameter(cmd, "@IdItemLote", DbType.Int32, int.Parse(Cotizacion.IdLote));
                DBCommon.dbConn.AddInParameter(cmd, "@rombo", DbType.String, Cotizacion.Rombo);
                DBCommon.dbConn.AddInParameter(cmd, "@docref_tipo", DbType.String, Cotizacion.TipoReferencia);
                DBCommon.dbConn.AddInParameter(cmd, "@docref_numero", DbType.String, Cotizacion.NumeroReferencia);
                DBCommon.dbConn.AddInParameter(cmd, "@IdCli2", DbType.Int32, int.Parse(Cotizacion.Aseguradora));
                DBCommon.dbConn.AddInParameter(cmd, "@deducible", DbType.Currency, decimal.Parse(Cotizacion.Deducible));
                DBCommon.dbConn.AddInParameter(cmd, "@deducibleminimo", DbType.Currency, decimal.Parse(Cotizacion.MinimoDeducible));
                DBCommon.dbConn.AddInParameter(cmd, "@motivo", DbType.Int32, Cotizacion.IdMotivo);
                DBCommon.dbConn.AddInParameter(cmd, "@valor_hora", DbType.Currency, decimal.Parse(Cotizacion.ValorHora));
                DBCommon.dbConn.AddInParameter(cmd, "@bloquear", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@id_plan", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@idCita", DbType.Int32, 0);            
                DBCommon.dbConn.ExecuteNonQuery(cmd, Tr);

                IdRetorno = int.Parse(DBCommon.dbConn.GetParameterValue(cmd, "@idRetorno").ToString());
                                             

                cmd = DBCommon.dbConn.GetStoredProcCommand("PutCotControlTotal");
                DBCommon.dbConn.AddInParameter(cmd, "@id", DbType.Int32, IdRetorno);
                DBCommon.dbConn.ExecuteNonQuery(cmd, Tr);

                Tr.Commit();

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

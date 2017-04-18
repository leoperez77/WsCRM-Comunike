﻿using System;
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
    public class DBPedido
    {
        public static wsControl PutPedido(Stream JSONdataStream)
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

                var Pedido = JsonConvert.DeserializeObject<wsPedido>(JSONdata);

                if (Pedido == null)
                {
                    // Error: Couldn't deserialize our JSON string into a "wsOrder" object.
                    throw new System.InvalidOperationException("Objeto JSON no pudo convertirse en pedido");
                }

                int IdRetorno = 0;
                DbCommand cmd = DBCommon.dbConn.GetStoredProcCommand("PutCotPedido2");
                DBCommon.dbConn.AddOutParameter(cmd, "@idRetorno", DbType.Int32, IdRetorno);
                DBCommon.dbConn.AddInParameter(cmd, "@emp", DbType.Int32, int.Parse(Pedido.IdEmpresa));
                DBCommon.dbConn.AddInParameter(cmd, "@usucrea", DbType.Int32, int.Parse(Pedido.Usuario));
                DBCommon.dbConn.AddInParameter(cmd, "@bod", DbType.Int32, int.Parse(Pedido.Bodega));
                DBCommon.dbConn.AddInParameter(cmd, "@usu", DbType.Int32, int.Parse(Pedido.Usuario) * -1);
                DBCommon.dbConn.AddInParameter(cmd, "@id", DbType.Int32, int.Parse(Pedido.Id));
                DBCommon.dbConn.AddInParameter(cmd, "@fecha", DbType.DateTime, DateTime.Parse(Pedido.Fecha));
                DBCommon.dbConn.AddInParameter(cmd, "@idtipo", DbType.Int32, int.Parse(Pedido.TipoDocumento));
                DBCommon.dbConn.AddInParameter(cmd, "@idcli", DbType.Int32, int.Parse(Pedido.Cliente));
                DBCommon.dbConn.AddInParameter(cmd, "@idvend", DbType.Int32, int.Parse(Pedido.Vendedor));
                DBCommon.dbConn.AddInParameter(cmd, "@idformapago", DbType.Int32, int.Parse(Pedido.FormaPago));
                DBCommon.dbConn.AddInParameter(cmd, "@idcontacto", DbType.Int32, int.Parse(Pedido.Contacto));               
                DBCommon.dbConn.AddInParameter(cmd, "@tot_Sub", DbType.Currency, decimal.Parse(Pedido.Subtotal));
                DBCommon.dbConn.AddInParameter(cmd, "@tot_Descuento", DbType.Currency, decimal.Parse(Pedido.Descuento));
                DBCommon.dbConn.AddInParameter(cmd, "@tot_Iva", DbType.Currency, decimal.Parse(Pedido.Iva));
                DBCommon.dbConn.AddInParameter(cmd, "@tot_Tot", DbType.Currency, decimal.Parse(Pedido.Subtotal));               
                DBCommon.dbConn.AddInParameter(cmd, "@moneda", DbType.Int32, int.Parse(Pedido.Moneda));
                DBCommon.dbConn.AddInParameter(cmd, "@Tasa", DbType.Currency, decimal.Parse(Pedido.Tasa));
                DBCommon.dbConn.AddInParameter(cmd, "@notas", DbType.String, Pedido.Notas);
                DBCommon.dbConn.AddInParameter(cmd, "@docref_tipo", DbType.String, Pedido.TipoReferencia);
                DBCommon.dbConn.AddInParameter(cmd, "@docref_numero", DbType.String, Pedido.NumeroReferencia);
                DBCommon.dbConn.AddInParameter(cmd, "@IdItemLote", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@notasinternas", DbType.String, Pedido.NotasInternas);
                DBCommon.dbConn.AddInParameter(cmd, "@fechaestimada", DbType.DateTime, DateTime.Parse(Pedido.FechaEstimada));
                DBCommon.dbConn.AddInParameter(cmd, "@id_med", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@id_ent", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@ret1", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@ret2", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@ret3", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@ret4", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@ret5", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@ret6", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@r1", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@r2", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@r3", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@r4", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@r5", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@r6", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@ajuste", DbType.Currency, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@id_orden_taller", DbType.Int32, Pedido.IdOrden);               
                DBCommon.dbConn.ExecuteNonQuery(cmd, Tr);

                IdRetorno = int.Parse(DBCommon.dbConn.GetParameterValue(cmd, "@idRetorno").ToString());

             
                int Linea = 0;

                foreach (var detalle in Pedido.Detalle)
                {
                    cmd = DBCommon.dbConn.GetStoredProcCommand("PutCotPedidoItems");
                    DBCommon.dbConn.AddInParameter(cmd, "@emp", DbType.Int32, int.Parse(Pedido.IdEmpresa));
                    DBCommon.dbConn.AddInParameter(cmd, "@bod", DbType.Int32, int.Parse(Pedido.Bodega));
                    DBCommon.dbConn.AddInParameter(cmd, "@usu", DbType.Int32, int.Parse(Pedido.Usuario));
                    DBCommon.dbConn.AddInParameter(cmd, "@renglon", DbType.Int32, Linea);
                    DBCommon.dbConn.AddInParameter(cmd, "@idcot", DbType.Int32, IdRetorno);
                    DBCommon.dbConn.AddInParameter(cmd, "@id", DbType.Int32, detalle.Id);
                    DBCommon.dbConn.AddInParameter(cmd, "@iditem", DbType.Int32, detalle.IdItem);
                    DBCommon.dbConn.AddInParameter(cmd, "@cant", DbType.Currency, detalle.Cantidad);
                    DBCommon.dbConn.AddInParameter(cmd, "@preciolista", DbType.Currency, detalle.Precio);
                    DBCommon.dbConn.AddInParameter(cmd, "@preciocotizado", DbType.Currency, detalle.PrecioCotizado);
                    DBCommon.dbConn.AddInParameter(cmd, "@iva", DbType.Currency, detalle.Iva);
                    DBCommon.dbConn.AddInParameter(cmd, "@notas", DbType.String, detalle.Notas);
                    DBCommon.dbConn.AddInParameter(cmd, "@preciomasiva", DbType.Currency, detalle.Subtotal);
                    DBCommon.dbConn.AddInParameter(cmd, "@porcentaje_descuento", DbType.Currency, detalle.Descuento);
                    DBCommon.dbConn.AddInParameter(cmd, "@porcentaje_descuento_grupo", DbType.Currency, -1);
                    DBCommon.dbConn.AddInParameter(cmd, "@ignorarstock", DbType.Int16, 0);
                    DBCommon.dbConn.AddInParameter(cmd, "@idlote", DbType.Int32, detalle.IdLote);
                    DBCommon.dbConn.AddInParameter(cmd, "@conv", DbType.Currency, detalle.Conversion);
                    DBCommon.dbConn.AddInParameter(cmd, "@und", DbType.Int16, 0);
                    DBCommon.dbConn.AddInParameter(cmd, "@descu_escal", DbType.Currency, 0);
                    DBCommon.dbConn.AddInParameter(cmd, "@iva2", DbType.Currency, 0);
                    DBCommon.dbConn.AddInParameter(cmd, "@id_iva2", DbType.Int16, 0);
                    DBCommon.dbConn.ExecuteNonQuery(cmd, Tr);
                    Linea++;
                }

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
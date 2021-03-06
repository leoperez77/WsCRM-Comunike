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

                var Orden = JsonConvert.DeserializeObject<wsOrdenTaller>(JSONdata);

                if (Orden == null)
                {
                    // Error: Couldn't deserialize our JSON string into a "wsOrder" object.
                    throw new System.InvalidOperationException("Objeto JSON no pudo convertirse en encabezado orden");
                }
                
                int IdRetorno = 0;
                DbCommand cmd = DBCommon.dbConn.GetStoredProcCommand("PutTalOrden");
                DBCommon.dbConn.AddOutParameter(cmd, "@idRetorno", DbType.Int32, IdRetorno);
                DBCommon.dbConn.AddInParameter(cmd, "@emp", DbType.Int32, int.Parse(Orden.IdEmpresa));
                DBCommon.dbConn.AddInParameter(cmd, "@usucrea", DbType.Int32, int.Parse(Orden.Usuario));
                DBCommon.dbConn.AddInParameter(cmd, "@bod", DbType.Int32, int.Parse(Orden.Bodega));
                DBCommon.dbConn.AddInParameter(cmd, "@usu", DbType.Int32, int.Parse(Orden.Usuario) ); //* -1
                DBCommon.dbConn.AddInParameter(cmd, "@id", DbType.Int32, int.Parse(Orden.Id));
                DBCommon.dbConn.AddInParameter(cmd, "@fecha", DbType.DateTime, DateTime.Parse(Orden.Fecha));
                DBCommon.dbConn.AddInParameter(cmd, "@idtipo", DbType.Int32, int.Parse(Orden.TipoDocumento));
                DBCommon.dbConn.AddInParameter(cmd, "@idcli", DbType.Int32, int.Parse(Orden.Cliente));
                DBCommon.dbConn.AddInParameter(cmd, "@idvend", DbType.Int32, int.Parse(Orden.Vendedor));
                DBCommon.dbConn.AddInParameter(cmd, "@idcontacto", DbType.Int32, int.Parse(Orden.Contacto));
                DBCommon.dbConn.AddInParameter(cmd, "@tot_Sub", DbType.Currency, decimal.Parse(Orden.Subtotal));
                DBCommon.dbConn.AddInParameter(cmd, "@tot_Descuento", DbType.Currency, decimal.Parse(Orden.Descuento));
                DBCommon.dbConn.AddInParameter(cmd, "@tot_Iva", DbType.Currency, decimal.Parse(Orden.Iva));
                DBCommon.dbConn.AddInParameter(cmd, "@tot_Tot", DbType.Currency, decimal.Parse(Orden.Subtotal));
                DBCommon.dbConn.AddInParameter(cmd, "@fechaestimada", DbType.DateTime, DateTime.Parse(Orden.FechaEstimada));
                DBCommon.dbConn.AddInParameter(cmd, "@notasinternas", DbType.String, Orden.NotasInternas);
                DBCommon.dbConn.AddInParameter(cmd, "@notas", DbType.String, Orden.Notas);
                DBCommon.dbConn.AddInParameter(cmd, "@Km", DbType.Int32, int.Parse(Orden.Kilometraje));
                DBCommon.dbConn.AddInParameter(cmd, "@IdItemLote", DbType.Int32, int.Parse(Orden.IdLote));
                DBCommon.dbConn.AddInParameter(cmd, "@rombo", DbType.String, Orden.Rombo);
                DBCommon.dbConn.AddInParameter(cmd, "@docref_tipo", DbType.String, Orden.TipoReferencia);
                DBCommon.dbConn.AddInParameter(cmd, "@docref_numero", DbType.String, Orden.NumeroReferencia);
                DBCommon.dbConn.AddInParameter(cmd, "@IdCli2", DbType.Int32, int.Parse(Orden.Aseguradora));
                DBCommon.dbConn.AddInParameter(cmd, "@deducible", DbType.Currency, decimal.Parse(Orden.Deducible));
                DBCommon.dbConn.AddInParameter(cmd, "@deducibleminimo", DbType.Currency, decimal.Parse(Orden.MinimoDeducible));
                DBCommon.dbConn.AddInParameter(cmd, "@motivo", DbType.Int32, Orden.IdMotivo);
                DBCommon.dbConn.AddInParameter(cmd, "@valor_hora", DbType.Currency, decimal.Parse(Orden.ValorHora));
                DBCommon.dbConn.AddInParameter(cmd, "@bloquear", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@id_plan", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@idCita", DbType.Int32, 0);            
                DBCommon.dbConn.ExecuteNonQuery(cmd, Tr);

                if (Orden.Id == "0")
                    IdRetorno = int.Parse(DBCommon.dbConn.GetParameterValue(cmd, "@idRetorno").ToString());
                else
                    IdRetorno = int.Parse(Orden.Id);

                //Actualizar el campo origen
                cmd = DBCommon.dbConn.GetStoredProcCommand("PutCM_OrigenOrdenes");
                DBCommon.dbConn.AddInParameter(cmd, "@idCotizacion", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@idPedido", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@idOrden", DbType.Int32, IdRetorno);
                DBCommon.dbConn.AddInParameter(cmd, "@Origen", DbType.String, Orden.Origen);
                DBCommon.dbConn.ExecuteNonQuery(cmd, Tr);

                foreach (var detalle in Orden.Detalle)
                {
                    cmd = DBCommon.dbConn.GetStoredProcCommand("PutTalOrdenItems");
                    DBCommon.dbConn.AddInParameter(cmd, "@emp", DbType.Int32, int.Parse(Orden.IdEmpresa));
                    DBCommon.dbConn.AddInParameter(cmd, "@bod", DbType.Int32, int.Parse(Orden.Bodega));
                    DBCommon.dbConn.AddInParameter(cmd, "@usu", DbType.Int32, int.Parse(Orden.Usuario));
                    DBCommon.dbConn.AddInParameter(cmd, "@idcot", DbType.Int32, IdRetorno);
                    DBCommon.dbConn.AddInParameter(cmd, "@id", DbType.Int32, detalle.Id);
                    DBCommon.dbConn.AddInParameter(cmd, "@iditem", DbType.Int32, detalle.IdItem);
                    DBCommon.dbConn.AddInParameter(cmd, "@cant", DbType.Currency, detalle.Cantidad);
                    DBCommon.dbConn.AddInParameter(cmd, "@tiempo", DbType.Currency, detalle.Tiempo);
                    DBCommon.dbConn.AddInParameter(cmd, "@valor_hora", DbType.Currency, detalle.ValorHora);
                    DBCommon.dbConn.AddInParameter(cmd, "@valor_ope", DbType.Currency, detalle.ValorOperacion);
                    DBCommon.dbConn.AddInParameter(cmd, "@iva", DbType.Currency, detalle.Iva);
                    DBCommon.dbConn.AddInParameter(cmd, "@notas", DbType.String, detalle.Notas);
                    DBCommon.dbConn.AddInParameter(cmd, "@porcentaje_descuento", DbType.Currency, detalle.PorcentajeDescuento);
                    DBCommon.dbConn.AddInParameter(cmd, "@id_operario", DbType.Int32, detalle.Operario);
                    DBCommon.dbConn.AddInParameter(cmd, "@facturar_a", DbType.String, detalle.Facturar);
                    DBCommon.dbConn.AddInParameter(cmd, "@tipo_operacion", DbType.String, detalle.TipoOperacion);
                    DBCommon.dbConn.AddInParameter(cmd, "@id_tal_chequeo", DbType.Int32, 0);
                    DBCommon.dbConn.AddInParameter(cmd, "@id_causal", DbType.Int32, 0);
                    DBCommon.dbConn.AddInParameter(cmd, "@id_sintoma", DbType.Int32, 0);
                    DBCommon.dbConn.AddInParameter(cmd, "@id_dano", DbType.Int32, 0);
                    DBCommon.dbConn.AddInParameter(cmd, "@gara_notas1", DbType.String, "");
                    DBCommon.dbConn.AddInParameter(cmd, "@gara_notas2", DbType.String, "");
                    DBCommon.dbConn.AddInParameter(cmd, "@estado_garantia", DbType.Int32, 0);
                    DBCommon.dbConn.AddInParameter(cmd, "@valor_garantia", DbType.Int32, 0);                    
                    DBCommon.dbConn.ExecuteNonQuery(cmd, Tr);                    
                }
                
                cmd = DBCommon.dbConn.GetStoredProcCommand("PutCotControlTotal");
                DBCommon.dbConn.AddInParameter(cmd, "@id", DbType.Int32, IdRetorno);
                DBCommon.dbConn.ExecuteNonQuery(cmd, Tr);

                cmd = DBCommon.dbConn.GetStoredProcCommand("PutTalActualizarTotalOrden");
                DBCommon.dbConn.AddInParameter(cmd, "@idorden", DbType.Int32, IdRetorno);
                DBCommon.dbConn.ExecuteNonQuery(cmd, Tr);

                cmd = DBCommon.dbConn.GetStoredProcCommand("CMGetLineasOrden");
                DBCommon.dbConn.AddInParameter(cmd, "@id", DbType.Int32, IdRetorno);
                var ds = DBCommon.dbConn.ExecuteDataSet(cmd, Tr);
                obj.Lineas = new List<wsResultado>();
              
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    var res = new wsResultado
                    {
                        Id = int.Parse(dr["id"].ToString()),
                        IdItem = int.Parse(dr["IdItem"].ToString()),
                        FacturarA = dr["Facturar"].ToString()
                    };

                    obj.Lineas.Add(res);
                }

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

        public static wsControl putEstadoOperacion(Stream JSONdataStream)
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

                var Operacion = JsonConvert.DeserializeObject<wsOperacion>(JSONdata);

                if (Operacion == null)
                {
                    throw new System.InvalidOperationException("Objeto JSON no pudo convertirse en Opercion");
                }
                
                DbCommand cmd = DBCommon.dbConn.GetStoredProcCommand("CMPut_EstadoOperacion");
                DBCommon.dbConn.AddInParameter(cmd, "@id_cot_cotizacion_item", DbType.Int32, int.Parse(Operacion.IdLineaOperacion));
                DBCommon.dbConn.AddInParameter(cmd, "@id_usuario_autorizo", DbType.Int32, int.Parse(Operacion.IdUsuario));
                DBCommon.dbConn.AddInParameter(cmd, "@autorizo", DbType.Int32, int.Parse(Operacion.Estado));
                DBCommon.dbConn.AddInParameter(cmd, "@id_con_cco", DbType.Int32, int.Parse(Operacion.IdCentro));
                
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

        public static wsControl putEjecucionOperacion(Stream JSONdataStream)
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

                var Operacion = JsonConvert.DeserializeObject<wsOperacion>(JSONdata);

                if (Operacion == null)
                {
                    throw new System.InvalidOperationException("Objeto JSON no pudo convertirse en Operacion");
                }

                DbCommand cmd = DBCommon.dbConn.GetStoredProcCommand("CMPut_EjecucionOperacion");
                DBCommon.dbConn.AddInParameter(cmd, "@id_cot_cotizacion_item", DbType.Int32, int.Parse(Operacion.IdLineaOperacion));
                DBCommon.dbConn.AddInParameter(cmd, "@que", DbType.Int32, int.Parse(Operacion.Estado));             
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

        public static wsOrdenTaller GetOrden(int IdOrden)
        {
            DbCommand cmd = DBCommon.dbConn.GetStoredProcCommand("CMGet_cot_cotizacion");
            DBCommon.dbConn.AddInParameter(cmd, "@IdCotizacion", DbType.Int32, IdOrden);

            var obj = new wsOrdenTaller();
            using (IDataReader dr = DBCommon.dbConn.ExecuteReader(cmd))
            {
                if (dr.Read())
                {
                    obj.Bodega = dr["bodega"].ToString();
                    obj.Cliente = dr["cliente"].ToString();
                    obj.Contacto = dr["contacto"].ToString();
                    obj.Descuento = dr["descuento"].ToString();                   
                    obj.Fecha = dr["fecha"].ToString();
                    obj.FechaEstimada = dr["fechaestimada"].ToString();                   
                    obj.Id = dr["id"].ToString();
                    obj.IdEmpresa = dr["idempresa"].ToString();                    
                    obj.Iva = dr["iva"].ToString();                    
                    obj.Notas = dr["notas"].ToString();
                    obj.NotasInternas = dr["notasinternas"].ToString();
                    obj.Numero = dr["numero"].ToString();
                    obj.NumeroReferencia = dr["numeroreferencia"].ToString();
                    obj.Subtotal = dr["subtotal"].ToString();                    
                    obj.TipoDocumento = dr["tipodocumento"].ToString();
                    obj.TipoReferencia = dr["tiporeferencia"].ToString();
                    obj.Total = dr["total"].ToString();
                    obj.Usuario = dr["usuario"].ToString();
                    obj.Vendedor = dr["vendedor"].ToString();
                    obj.Origen = dr["origen"].ToString();
                }
                dr.Close();
            }

            cmd = DBCommon.dbConn.GetStoredProcCommand("CMGet_cot_cotizacion_item");
            DBCommon.dbConn.AddInParameter(cmd, "@IdCotizacion", DbType.Int32, IdOrden);
            using (IDataReader dr = DBCommon.dbConn.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    var det = new wsOperacionOrden();
                    det.Cantidad = dr["cantidad"].ToString();                   
                    det.Id = dr["id"].ToString();
                    det.IdCotizacion = dr["idcotizacion"].ToString();
                    det.IdItem = dr["iditem"].ToString();
                    det.Iva = dr["iva"].ToString();
                    det.Notas = dr["notas"].ToString();
                    det.ValorHora = dr["descu_escal"].ToString();
                    det.ValorOperacion = dr["can_tot_dis"].ToString();
                    det.Renglon = dr["renglon"].ToString();       
                    det.Facturar = dr["facturar_a"].ToString();
                    det.Operario = dr["id_operario"].ToString();
                    obj.Detalle.Add(det);
                }
            }

            return obj;
        }

        public static wsControl putItemOrden(Stream JSONdataStream)
        {
            wsControl obj = new wsControl();
            obj.FechaHora = DateTime.Now.ToString();
            obj.Origen = System.Reflection.MethodBase.GetCurrentMethod().Name;

            SqlConnection Conn = (SqlConnection)DBCommon.dbConn.CreateConnection();
            Conn.Open();
            SqlTransaction Tr = Conn.BeginTransaction();

            try
            {
                StreamReader reader = new StreamReader(JSONdataStream);
                string JSONdata = reader.ReadToEnd();

                var Orden = JsonConvert.DeserializeObject<wsOrdenTaller>(JSONdata);

                if (Orden == null)
                {
                    // Error: Couldn't deserialize our JSON string into a "wsOrder" object.
                    throw new System.InvalidOperationException("Objeto JSON no pudo convertirse en encabezado orden");
                }

                foreach (var detalle in Orden.Detalle)
                {
                   var  cmd = DBCommon.dbConn.GetStoredProcCommand("PutTalOrdenItems");
                    DBCommon.dbConn.AddInParameter(cmd, "@emp", DbType.Int32, int.Parse(Orden.IdEmpresa));
                    DBCommon.dbConn.AddInParameter(cmd, "@bod", DbType.Int32, int.Parse(Orden.Bodega));
                    DBCommon.dbConn.AddInParameter(cmd, "@usu", DbType.Int32, int.Parse(Orden.Usuario));
                    DBCommon.dbConn.AddInParameter(cmd, "@idcot", DbType.Int32, int.Parse(Orden.Id));
                    DBCommon.dbConn.AddInParameter(cmd, "@id", DbType.Int32, detalle.Id);
                    DBCommon.dbConn.AddInParameter(cmd, "@iditem", DbType.Int32, detalle.IdItem);
                    DBCommon.dbConn.AddInParameter(cmd, "@cant", DbType.Currency, detalle.Cantidad);
                    DBCommon.dbConn.AddInParameter(cmd, "@tiempo", DbType.Currency, detalle.Tiempo);
                    DBCommon.dbConn.AddInParameter(cmd, "@valor_hora", DbType.Currency, detalle.ValorHora);
                    DBCommon.dbConn.AddInParameter(cmd, "@valor_ope", DbType.Currency, detalle.ValorOperacion);
                    DBCommon.dbConn.AddInParameter(cmd, "@iva", DbType.Currency, detalle.Iva);
                    DBCommon.dbConn.AddInParameter(cmd, "@notas", DbType.String, detalle.Notas);
                    DBCommon.dbConn.AddInParameter(cmd, "@porcentaje_descuento", DbType.Currency, detalle.PorcentajeDescuento);
                    DBCommon.dbConn.AddInParameter(cmd, "@id_operario", DbType.Int32, detalle.Operario);
                    DBCommon.dbConn.AddInParameter(cmd, "@facturar_a", DbType.String, detalle.Facturar);
                    DBCommon.dbConn.AddInParameter(cmd, "@tipo_operacion", DbType.String, detalle.TipoOperacion);
                    DBCommon.dbConn.AddInParameter(cmd, "@id_tal_chequeo", DbType.Int32, 0);
                    DBCommon.dbConn.AddInParameter(cmd, "@id_causal", DbType.Int32, 0);
                    DBCommon.dbConn.AddInParameter(cmd, "@id_sintoma", DbType.Int32, 0);
                    DBCommon.dbConn.AddInParameter(cmd, "@id_dano", DbType.Int32, 0);
                    DBCommon.dbConn.AddInParameter(cmd, "@gara_notas1", DbType.String, "");
                    DBCommon.dbConn.AddInParameter(cmd, "@gara_notas2", DbType.String, "");
                    DBCommon.dbConn.AddInParameter(cmd, "@estado_garantia", DbType.Int32, 0);
                    DBCommon.dbConn.AddInParameter(cmd, "@valor_garantia", DbType.Int32, 0);
                    DBCommon.dbConn.ExecuteNonQuery(cmd, Tr);
                }

                var cmd2 = DBCommon.dbConn.GetStoredProcCommand("CMGet_id_item_orden");
                DBCommon.dbConn.AddInParameter(cmd2, "@IdOrden", DbType.Int32, int.Parse(Orden.Id));
                var ds = DBCommon.dbConn.ExecuteDataSet(cmd2, Tr);
                if (ds.Tables[0].Rows.Count > 0)
                    obj.IdGenerado = ds.Tables[0].Rows[0]["id"].ToString();
                else
                    obj.IdGenerado = "1";

                cmd2 = DBCommon.dbConn.GetStoredProcCommand("CMGetLineasOrden");
                DBCommon.dbConn.AddInParameter(cmd2, "@id", DbType.Int32, Orden.Id);
                ds = DBCommon.dbConn.ExecuteDataSet(cmd2, Tr);
                
                obj.Lineas = new List<wsResultado>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    var res = new wsResultado
                    {
                        Id = int.Parse(dr["id"].ToString()),
                        IdItem = int.Parse(dr["IdItem"].ToString()),
                        FacturarA = dr["Facturar"].ToString()
                    };

                    obj.Lineas.Add(res);
                }

                Tr.Commit();

                
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

        public static wsControl DelItemOrden(int IdLinea)
        {
            wsControl obj = new wsControl
            {
                FechaHora = DateTime.Now.ToString(),
                Origen = System.Reflection.MethodBase.GetCurrentMethod().Name
            };

            SqlConnection Conn = (SqlConnection)DBCommon.dbConn.CreateConnection();

            try
            {                
                Conn.Open();
                var cmd = DBCommon.dbConn.GetStoredProcCommand("CM_DelItemOrden");
                DBCommon.dbConn.AddInParameter(cmd, "@idLinea", DbType.Int32, IdLinea);
                DBCommon.dbConn.ExecuteNonQuery(cmd);
                obj.IdGenerado = "1";
                obj.Estado = "Ok";
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
            finally
            {
                Conn.Close();
            }
            return obj;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Web.Script.Serialization;
using smdcrmws.dto;
namespace sdmcrmws.data
{
    public class DBcliente
    {
        public static List<wsCliente> GetClientesSincronizar(string IdEmpresa)
        {
            List<wsCliente> results = new List<wsCliente>();

            DbCommand cmd = DBCommon.dbConn.GetStoredProcCommand("CMGetcot_clientes");
            DBCommon.dbConn.AddInParameter(cmd, "@id_emp", DbType.Int16, int.Parse(IdEmpresa));

            //((RefCountingDataReader)db.ExecuteReader(command)).InnerReader as SqlDataReader;
            using (IDataReader dr = DBCommon.dbConn.ExecuteReader(cmd))
            {
                while(dr.Read())
                {
                    wsCliente obj = new wsCliente();
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

        public static wsControl PutClienteSincronizar(Stream JSONdataStream)
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
                wsCliente cliente = jss.Deserialize<wsCliente>(JSONdata);
                if (cliente == null)
                {                  
                    throw new System.InvalidOperationException("Objeto JSON no pudo convertirse en objeto wsCliente");                   
                }

                DbCommand cmd = DBCommon.dbConn.GetStoredProcCommand("CMSynccot_clientes");
                DBCommon.dbConn.AddInParameter(cmd, "@id", DbType.Int32, int.Parse(cliente.Campo_1));
               
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
                if(ex.InnerException != null)
                {
                    obj.Error += Environment.NewLine + ex.InnerException.Message;
                }
            }
           
            return obj;
        }

        public static wsControl PutClientesSincronizar(Stream JSONdataStream)
        {
            /*
             	campo_1 = id_emp
	            campo_2 = id
	            campo_3 = id_cot_zona_sub
	            campo_4 = razon_social
	            campo_5 = tel_1
	            campo_6 = tel_2
	            campo_7 = direccion
	            campo_8 = url
	            campo_9 = id_cot_estado
	            campo_10 = id_usuario_vendedor
	            campo_11 = id_cot_cliente_actividad
	            campo_12 = id_cot_cliente_origen
	            campo_13 = nit
	            campo_14 = notas
	            campo_15 = privado
	            campo_16 = ventas1
	            campo_17 = ventas2
	            campo_18 = utilidad1
	            campo_19 = utilidad2
	            campo_20 = id_cot_cliente_tipo
	            campo_21 = id_cot_cliente_perfil
	            campo_22 = cupo_credito
	            campo_23 = precio_costo
	            campo_24 = id_tipo_tributario
	            campo_25 = digito
	            campo_26 = id_cot_cliente_contacto
	            campo_27 = id_cot_forma_pago
	            campo_28 = id_cot_item_listas
	            campo_29 = id_cot_tipo
	            campo_30 = id_cot_cotizacion_formatos
	            campo_31 = id_con_cco
	            campo_32 = permite_controlados
	            campo_33 = id_cot_cliente_pais
	            campo_34 = fecha_modificacion
	            campo_35 = clave_web
	            campo_36 = recibir_mail
	            campo_37 = impedir_descuentos_pie
	            campo_38 = idv
	            campo_39 = fecha_creacion
	            campo_40 = id_cot_bodega
	            campo_41 = fletes
	            campo_42 = id_emp_orig
	            campo_43 = dias_gracia
	            campo_44 = id_tipo_tributario2
	            campo_45 = max_dcto
	            campo_46 = id_cot_bodega_recep
	            campo_47 = descu_escal
	            campo_48 = dia_max
	            campo_49 = control_unidades
	            campo_50 = codigo
	            campo_51 = no_iva
	            campo_52 = no_sismed
	            campo_53 = cupo_credito_tal
	            campo_54 = cupo_credito_veh
	            campo_55 = ppal
	            campo_56 = def_taller
	            campo_57 = '',
	            campo_58 = '',
	            campo_59 = '',
	            campo_60 = ''              
             */

            wsControl obj = new wsControl();
            obj.FechaHora = DateTime.Now.ToString();
            obj.Origen = System.Reflection.MethodBase.GetCurrentMethod().Name;

            try
            {
                // Read in our Stream into a string...
                StreamReader reader = new StreamReader(JSONdataStream);
                string JSONdata = reader.ReadToEnd();
                               
                JavaScriptSerializer jss = new JavaScriptSerializer();
                var clientes = jss.Deserialize<List<wsCliente>>(JSONdata);
                
                if (clientes == null)
                {
                    // Error: Couldn't deserialize our JSON string into a "wsOrder" object.
                    throw new System.InvalidOperationException("Objeto JSON no pudo convertirse en arreglo de objetos wsCliente");
                }

                for (int i = 0; i < clientes.Count; i++)
                {
                    wsCliente cliente = clientes[i];

                    DbCommand cmd = DBCommon.dbConn.GetStoredProcCommand("CMSynccot_cliente");
                    DBCommon.dbConn.AddInParameter(cmd, "@id", DbType.Int32, int.Parse(cliente.Campo_1));
                    
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
                obj.Error += ex.Message;
                if (ex.InnerException != null)
                {
                    obj.Error += Environment.NewLine + ex.InnerException.Message;
                }
            }

            return obj;
        }
        
        public static wsControl MarcarClienteSincronizar(string IdCliente)
        {
            wsControl obj = new wsControl();
            obj.FechaHora = DateTime.Now.ToString();
            obj.Origen = System.Reflection.MethodBase.GetCurrentMethod().Name;

            try
            {
                DbCommand cmd = DBCommon.dbConn.GetStoredProcCommand("CMSynccot_cliente");
                DBCommon.dbConn.AddInParameter(cmd, "@id", DbType.Int32, int.Parse(IdCliente));

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
                
        public static List<wsContacto> GetContactosSincronizar(string IdEmpresa)
        {
            List<wsContacto> results = new List<wsContacto>();

            DbCommand cmd = DBCommon.dbConn.GetStoredProcCommand("CMGetcot_cliente_contactos");
            DBCommon.dbConn.AddInParameter(cmd, "@id_emp", DbType.Int16, int.Parse(IdEmpresa));

            //((RefCountingDataReader)db.ExecuteReader(command)).InnerReader as SqlDataReader;
            using (IDataReader dr = DBCommon.dbConn.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    wsContacto obj = new wsContacto();
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

        public static wsControl MarcarContactoSincronizar(string IdContacto)
        {
            wsControl obj = new wsControl();
            obj.FechaHora = DateTime.Now.ToString();
            obj.Origen = System.Reflection.MethodBase.GetCurrentMethod().Name;

            try
            {
                DbCommand cmd = DBCommon.dbConn.GetStoredProcCommand("CMSynccot_contacto");
                DBCommon.dbConn.AddInParameter(cmd, "@id", DbType.Int32, int.Parse(IdContacto));

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

        public static wsControl PutCliente(Stream JSONdataStream)
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
                wsCliente cliente = jss.Deserialize<wsCliente>(JSONdata);
                if (cliente == null)
                {
                    throw new System.InvalidOperationException("Objeto JSON no pudo convertirse en objeto wsCliente");
                }

                DbCommand cmd = DBCommon.dbConn.GetStoredProcCommand("PutCotClientes4");
                DBCommon.dbConn.AddInParameter(cmd, "@emp", DbType.Int32, int.Parse(cliente.Campo_41));
                DBCommon.dbConn.AddInParameter(cmd, "@usu", DbType.Int32, Helper.Bcero(cliente.Campo_24));//
                DBCommon.dbConn.AddInParameter(cmd, "@idcli", DbType.Int32, int.Parse(cliente.Campo_1));//
                DBCommon.dbConn.AddInParameter(cmd, "@privado", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@zona", DbType.Int32, Helper.Bcero(cliente.Campo_43));
                DBCommon.dbConn.AddInParameter(cmd, "@raz", DbType.String, cliente.Campo_8);//
                DBCommon.dbConn.AddInParameter(cmd, "@tel1", DbType.String, cliente.Campo_9);//
                DBCommon.dbConn.AddInParameter(cmd, "@tel2", DbType.String, cliente.Campo_10);//
                DBCommon.dbConn.AddInParameter(cmd, "@dir", DbType.String, cliente.Campo_14);//
                DBCommon.dbConn.AddInParameter(cmd, "@url", DbType.String, cliente.Campo_46);
                DBCommon.dbConn.AddInParameter(cmd, "@est", DbType.Int16, Helper.Bcero(cliente.Campo_34));
                DBCommon.dbConn.AddInParameter(cmd, "@vend", DbType.Int32, Helper.Bcero(cliente.Campo_23));//
                DBCommon.dbConn.AddInParameter(cmd, "@act", DbType.Int32, Helper.Bcero(cliente.Campo_47));
                DBCommon.dbConn.AddInParameter(cmd, "@ori", DbType.Int32, Helper.Bcero(cliente.Campo_48));
                DBCommon.dbConn.AddInParameter(cmd, "@tipclie", DbType.Int32, Helper.Bcero(cliente.Campo_22));//                
                DBCommon.dbConn.AddInParameter(cmd, "@tipperfil", DbType.Int32, Helper.Bcero(cliente.Campo_44));//            
                DBCommon.dbConn.AddInParameter(cmd, "@nit", DbType.String, cliente.Campo_2);//
                DBCommon.dbConn.AddInParameter(cmd, "@ven1", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@ven2", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@uti1", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@uti2", DbType.Int32, 0);
                DBCommon.dbConn.AddInParameter(cmd, "@notas", DbType.String, cliente.Campo_28);//              
                DBCommon.dbConn.AddInParameter(cmd, "@cupo", DbType.Int32, Helper.Bcero(cliente.Campo_33));//
                DBCommon.dbConn.AddInParameter(cmd, "@IdTipoTributario", DbType.Int32, Helper.Bcero(cliente.Campo_6));//
                DBCommon.dbConn.AddInParameter(cmd, "@digito", DbType.Int32, Helper.Bcero(cliente.Campo_4));//
                DBCommon.dbConn.AddInParameter(cmd, "@idcontacto", DbType.Int32, Helper.Bcero(cliente.Campo_45));
                DBCommon.dbConn.AddInParameter(cmd, "@codigo", DbType.String, cliente.Campo_3);//
                DBCommon.dbConn.AddInParameter(cmd, "@id_cot_cliente_pais", DbType.Int32, Helper.Bcero(cliente.Campo_19));//
                DBCommon.dbConn.AddInParameter(cmd, "@FormaPago", DbType.Int32, Helper.Bcero(cliente.Campo_42));//
                DBCommon.dbConn.AddInParameter(cmd, "@nom1", DbType.String, cliente.Campo_36);
                DBCommon.dbConn.AddInParameter(cmd, "@nom2", DbType.String, cliente.Campo_37);
                DBCommon.dbConn.AddInParameter(cmd, "@ape1", DbType.String, cliente.Campo_39);
                DBCommon.dbConn.AddInParameter(cmd, "@ape2", DbType.String, cliente.Campo_40);
                DBCommon.dbConn.AddInParameter(cmd, "@IdTipoTributario2", DbType.String, Helper.Bcero(cliente.Campo_49));
                DBCommon.dbConn.AddInParameter(cmd, "@tipo_identificacion", DbType.String, cliente.Campo_5);


                try
                {
                    DBCommon.dbConn.ExecuteNonQuery(cmd);
                }
                catch
                {
                    throw;
                }

                //Actualizar la información que pertenece al contacto principal. Mail
                DbCommand cmdc = DBCommon.dbConn.GetStoredProcCommand("CMPut_contacto");
                DBCommon.dbConn.AddInParameter(cmdc, "@id_cot_cliente", DbType.Int32, cliente.Campo_1);
                DBCommon.dbConn.AddInParameter(cmdc, "@id", DbType.Int32, Helper.Bcero(cliente.Campo_45));
                DBCommon.dbConn.AddInParameter(cmdc, "@email", DbType.String, cliente.Campo_15);
                DBCommon.dbConn.AddInParameter(cmdc, "@fecha_cumple", DbType.String, cliente.Campo_27);
                DBCommon.dbConn.ExecuteNonQuery(cmdc);
                               

                if (int.Parse(cliente.Campo_1)==0)
                {
                    
                    DbCommand cmdcl = DBCommon.dbConn.GetStoredProcCommand("GetCotClientes");
                    DBCommon.dbConn.AddInParameter(cmdcl, "@id", DbType.Int32, 0);
                    DBCommon.dbConn.AddInParameter(cmdcl, "@Nit", DbType.String, cliente.Campo_3);
                    DBCommon.dbConn.AddInParameter(cmdcl, "@Emp", DbType.Int32, int.Parse(cliente.Campo_41));
                   
                    DataSet dsc = DBCommon.dbConn.ExecuteDataSet(cmdcl);
                    obj.IdGenerado = int.Parse(dsc.Tables[0].Rows[0]["id"].ToString()).ToString();

                    DbCommand cmdContacto = DBCommon.dbConn.GetStoredProcCommand("PutCotClienteContacto2");
                    DBCommon.dbConn.AddInParameter(cmdContacto, "@emp", DbType.Int32, int.Parse(cliente.Campo_41));
                    DBCommon.dbConn.AddInParameter(cmdContacto, "@usu", DbType.Int32, Helper.Bcero(cliente.Campo_24));
                    DBCommon.dbConn.AddInParameter(cmdContacto, "@idcli", DbType.Int32, int.Parse(obj.IdGenerado));
                    DBCommon.dbConn.AddInParameter(cmdContacto, "@idCont", DbType.Int32, 0);
                    DBCommon.dbConn.AddInParameter(cmdContacto, "@Nomb", DbType.String, cliente.Campo_8);
                    DBCommon.dbConn.AddInParameter(cmdContacto, "@tel1", DbType.String, cliente.Campo_9);
                    DBCommon.dbConn.AddInParameter(cmdContacto, "@reemp", DbType.String, "");
                    DBCommon.dbConn.AddInParameter(cmdContacto, "@carg", DbType.String, "");
                    DBCommon.dbConn.AddInParameter(cmdContacto, "@email", DbType.String, cliente.Campo_15);
                    DBCommon.dbConn.AddInParameter(cmdContacto, "@jefe", DbType.Int32, 0);
                    DBCommon.dbConn.AddInParameter(cmdContacto, "@prof", DbType.Int32, 0);
                    DBCommon.dbConn.AddInParameter(cmdContacto, "@anul", DbType.Int32, 0);
                    DBCommon.dbConn.AddInParameter(cmdContacto, "@sexo", DbType.Int32, 0);
                    DBCommon.dbConn.AddInParameter(cmdContacto, "@fechcumple", DbType.Int16, cliente.Campo_27.Substring(4));
                    DBCommon.dbConn.AddInParameter(cmdContacto, "@estcivil", DbType.Int32, 0);
                    DBCommon.dbConn.AddInParameter(cmdContacto, "@canthijos", DbType.Int32, 0);
                    DBCommon.dbConn.AddInParameter(cmdContacto, "@dir", DbType.String, cliente.Campo_14);
                    DBCommon.dbConn.AddInParameter(cmdContacto, "@cedula", DbType.String, cliente.Campo_3);
                    DBCommon.dbConn.ExecuteNonQuery(cmdContacto);

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

        public static List<wsMaestro> GetEstadistica(int IdCliente, DateTime FechaDesde, DateTime FechaHasta)
        {      
            List<wsMaestro> results = new List<wsMaestro>();
            DbCommand cmd = DBCommon.dbConn.GetStoredProcCommand("CMGet_estadistica_cliente");
            DBCommon.dbConn.AddInParameter(cmd, "@id_cliente", DbType.Int32, IdCliente);
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

        public static List<wsPedido> GetPedidos(int IdCliente, DateTime FechaDesde, DateTime FechaHasta)
        {
            List<wsPedido> li = new List<wsPedido>();

            DbCommand cmd = DBCommon.dbConn.GetStoredProcCommand("EcomGetPedidosCliente");
            DBCommon.dbConn.AddInParameter(cmd, "@IdCliente", DbType.Int32, IdCliente);
            DBCommon.dbConn.AddInParameter(cmd, "@Desde", DbType.Date, FechaDesde);
            DBCommon.dbConn.AddInParameter(cmd, "@Hasta", DbType.Date, FechaHasta);

            using (IDataReader dr = DBCommon.dbConn.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    var obj = new wsPedido();
                    obj.Bodega = dr["bodega"].ToString();
                    obj.Cliente = dr["cliente"].ToString();
                    obj.Contacto = dr["contacto"].ToString();
                    obj.Descuento = dr["descuento"].ToString();
                    obj.Dias = dr["dias"].ToString();
                    obj.Estado = dr["estado"].ToString();
                    obj.Factibilidad = dr["factibilidad"].ToString();
                    obj.Fecha = dr["fecha"].ToString();
                    obj.FechaEstimada = dr["fechaestimada"].ToString();
                    obj.FormaPago = dr["formapago"].ToString();
                    obj.Id = dr["id"].ToString();
                    obj.IdEmpresa = dr["idempresa"].ToString();
                    obj.IdOrden = dr["idorden"].ToString();
                    obj.Iva = dr["iva"].ToString();
                    obj.Moneda = dr["moneda"].ToString();
                    obj.Notas = dr["notas"].ToString();
                    obj.NotasInternas = dr["notasinternas"].ToString();
                    obj.Numero = dr["numero"].ToString();
                    obj.NumeroReferencia = dr["numeroreferencia"].ToString();
                    obj.Subtotal = dr["subtotal"].ToString();
                    obj.Tasa = dr["tasa"].ToString();
                    obj.TipoDocumento = dr["tipodocumento"].ToString();
                    obj.TipoReferencia = dr["tiporeferencia"].ToString();
                    obj.Total = dr["total"].ToString();
                    obj.Usuario = dr["usuario"].ToString();
                    obj.Vendedor = dr["vendedor"].ToString();
                    li.Add(obj);
                }
                dr.Close();
            }

            return li;
        }

       
    }
}

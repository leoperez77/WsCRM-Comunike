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

                    DbCommand cmd = DBCommon.dbConn.GetStoredProcCommand("CMSynccot_clientes");
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

        //public static DataTable GetItemByCodigo(string Codigo, int IdEmpresa)
        //{
        //    DbCommand cmd = DBCommon.dbConn.GetStoredProcCommand("GetItemByCodigo");
        //    DBCommon.dbConn.AddInParameter(cmd, "Codigo", DbType.String, Codigo);
        //    DBCommon.dbConn.AddInParameter(cmd, "IdEmpresa", DbType.Int32, IdEmpresa);
        //    return DBCommon.dbConn.ExecuteDataSet(cmd).Tables[0];
        //}
    }
}

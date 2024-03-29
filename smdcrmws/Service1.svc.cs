﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using smdcrmws.dto;
using sdmcrmws.data;
namespace smdcrmws
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class SMDCRM : ISmdcrmws
    {
      
        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public List<wsCliente> GetClientesSync(string IdEmpresa)
        {
            try
            {
                return DBcliente.GetClientesSincronizar(IdEmpresa);

            }
            catch (Exception ex)
            {
                //  Return any exception messages back to the Response header
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.StatusDescription = ex.Message.Replace("\r\n", "");
                return null;
            }
        }

     
        public wsControl PutClienteSync(Stream JSONdataStream)
        {
            return DBcliente.PutClienteSincronizar(JSONdataStream);
        }
        
        public wsControl PutClientesSync(Stream JSONdataStream)
        {
            return DBcliente.PutClientesSincronizar(JSONdataStream);
        }

        public List<wsOperario> GetOperariosSync(string IdEmpresa)
        {
            try
            {
                return DBUsuario.GetOperarios(IdEmpresa);

            }
            catch (Exception ex)
            {
                //  Return any exception messages back to the Response header
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.StatusDescription = ex.Message.Replace("\r\n", "");
                return null;
            }
        }

        public List<wsItem> GetItemsSync(string IdEmpresa)
        {
            try
            {
                return DBItem.GetItemsSincronizar(IdEmpresa,0);

            }
            catch (Exception ex)
            {
                //  Return any exception messages back to the Response header
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.StatusDescription = ex.Message.Replace("\r\n", "");
                return null;
            }
        }

        public wsControl PutItemsSync(Stream JSONdataStream)
        {
            return DBItem.PutItemsSincronizar(JSONdataStream);
        }

        public List<wsMaestro> GetTiposTributariosSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(int.Parse(IdEmpresa), "CMGettipo_tributarios");
        }

        public List<wsMaestro> GetPaisesSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(int.Parse(IdEmpresa), "CMGetcot_cliente_paises");
        }

        public List<wsMaestro> GetBarriosSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(int.Parse(IdEmpresa), "CMGetcot_cliente_barrios");
        }

        public List<wsMaestro> GetEstadosClienteSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(int.Parse(IdEmpresa), "CMGetcot_estados");
        }

        public List<wsMaestro> GetGruposSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(int.Parse(IdEmpresa), "CMGetcot_grupos");
        }

        public List<wsMaestro> GetSubgruposSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(int.Parse(IdEmpresa), "CMGetcot_grupo_subs");
        }

        public List<wsMaestro> GetBodegasSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(int.Parse(IdEmpresa), "CMGetcot_bodegas");
        }

        public List<wsMaestro> GetTallasSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(int.Parse(IdEmpresa), "CMGetcot_item_tallas");
        }

        public List<wsMaestro> GetColoresSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(int.Parse(IdEmpresa), "CMGetcot_item_colores");
        }

        public List<wsMaestro> GetCategoriasSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(int.Parse(IdEmpresa), "CMGetcot_item_categorias");
        }

        public List<wsMaestro> GetUnidadesSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(int.Parse(IdEmpresa), "CMGetcot_unidades");
        }

        public wsControl MarcarClienteSync(string IdCliente)
        {
            return DBcliente.MarcarClienteSincronizar(IdCliente);
        }

        public wsControl MarcarItemSync(string IdItem)
        {
            return DBItem.MarcarItemSincronizado(int.Parse(IdItem));
        }

        public List<wsContacto> GetContactosSync(string IdEmpresa)
        {
            return DBcliente.GetContactosSincronizar(IdEmpresa);
        }

        public List<wsMaestro> GetBancosSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(int.Parse(IdEmpresa), "CMGettes_bancos");
        }

        public List<wsMaestro> GetProfesionesSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(int.Parse(IdEmpresa), "CMGetcot_profesiones");            
        }

        public List<wsMaestro> GetCargosSync(string IdEmpresa)
        {            
            return DBMaestro.GetMaestro(int.Parse(IdEmpresa), "CMGetcot_cliente_cargos");
        }

        public wsControl MarcarContactoSync(string IdContacto)
        {
            return DBcliente.MarcarContactoSincronizar(IdContacto);
        }

        public List<wsMaestro> GetActividadesSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(int.Parse(IdEmpresa), "CMGetcot_cliente_actividades");
        }

        public List<wsMaestro> GetFormasPagoSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(int.Parse(IdEmpresa), "CMGetcot_formas_pago");
        }

        public List<wsMaestro> GetTiposClienteSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(int.Parse(IdEmpresa), "CMGetcot_cliente_tipos");
        }

        public List<wsMaestro> GetPerfilesClienteSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(int.Parse(IdEmpresa), "CMGetcot_cliente_perfiles");
        }

      
        public List<wsMaestro> GetSubgrupos4Sync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(int.Parse(IdEmpresa), "CMGetcot_grupo_subgrupos4");
        }

        public List<wsMaestro> GetSubgrupos5Sync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(int.Parse(IdEmpresa), "CMGetcot_grupo_subgrupos5");
        }

        public List<wsMaestro> GetZonasSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(int.Parse(IdEmpresa), "CMGetcot_zonas");
        }

        public List<wsMaestro> GetSubzonasSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(int.Parse(IdEmpresa), "CMGetcot_zona_subs");
        }

        public List<wsMaestro> GetMarcasVehSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(int.Parse(IdEmpresa), "CMGetveh_marcas");
        }

        public List<wsMaestro> GetLineasVehSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(int.Parse(IdEmpresa), "CMGetveh_lineas");
        }

        public List<wsMaestro> GetClasesVehSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(int.Parse(IdEmpresa), "CMGetveh_clases");
        }

        public List<wsMaestro> GetTiposServicioVehSync(string IdEmpresa)
        {            
            return DBMaestro.GetMaestro(int.Parse(IdEmpresa), "CMGetveh_servicios");
        }

        public List<wsMaestro> GetSubgrupos3Sync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(int.Parse(IdEmpresa), "CMGetcot_grupo_subgrupos3");
        }


        public List<wsLineaModelo> GetModelosLineaVehSync(string IdEmpresa)
        {
            return DBVehiculos.GetModelosLinea(IdEmpresa);
        }

        public wsControl MarcarModeloLineaSync(string IdModeloLinea)
        {
            return DBVehiculos.MarcarModeloLineaSincronizado(int.Parse(IdModeloLinea));
        }

        public List<wsItem> GetModelosAnoSync(string IdEmpresa)
        {
            try
            {
                return DBItem.GetItemsSincronizar(IdEmpresa, 1);

            }
            catch (Exception ex)
            {
                //  Return any exception messages back to the Response header
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.StatusDescription = ex.Message.Replace("\r\n", "");
                return null;
            }
        }

        public wsControl PutEncabezadoHn(Stream JSONdataStream)
        {
            return DBNegocio.PutEncabezadoNegocio(JSONdataStream);
        }

        public wsControl PutCotizacion(Stream JSONdataStream)
        {
            return DBNegocio.PutCotizacion(JSONdataStream);
        }

        public wsControl PutCliente(Stream JSONdataStream)
        {
            try
            {
                return DBcliente.PutCliente(JSONdataStream);
            }
            catch (Exception ex)
            {
                //  Return any exception messages back to the Response header
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.StatusDescription = (ex.Message + ex.InnerException.Message).Replace("\r\n", "");
                return null;
            }
        }

        public wsControl PutCotCliente(Stream JSONdataStream)
        {
            return DBcliente.PutCliente(JSONdataStream);
        }

        public List<wsVehiculo> GetVehiculosSync(string IdEmpresa)
        {
            return DBVehiculos.GetVehiculos(IdEmpresa);
        }

        public wsControl MarcarVehiculoSync(string IdVehiculo)
        {
            return DBVehiculos.MarcarVehiculoSincronizado(int.Parse(IdVehiculo));
        }

        public wsControl PutOrdenTaller(Stream JSONdataStream)
        {
            return DBOrden.PutOrden(JSONdataStream);
        }

        public List<wsMaestro> GetMotivosIngresoSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(int.Parse(IdEmpresa), "CMGettal_motivo_ingresos");
        }

        public wsControl PutPedido(Stream JSONdataStream)
        {
            return DBPedido.PutPedido(JSONdataStream);
        }

        public wsStock GetStock(string IdItem, string IdBodega)
        {
            return DBItem.GetStock(int.Parse(IdItem), int.Parse(IdBodega));
        }

        public List<wsMaestro> GetPlanesSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(int.Parse(IdEmpresa), "CMGettal_planes");
        }

        public List<wsMaestro> GetCampsSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(int.Parse(IdEmpresa), "CMGettal_campañas");
        }

        public wsControl MarcarCampSync(string IdCamp)
        {
            return DBMaestro.MarcarMaestroSincronizado(int.Parse(IdCamp), "CMSynctal_camp_enc");
        }

        public List<wsMaestro> GetEstadisticaCliente(string IdCliente, string FechaDesde, string FechaHasta)
        {
            try
            { 
                return DBcliente.GetEstadistica(int.Parse(IdCliente),DateTime.Parse(FechaDesde), DateTime.Parse(FechaHasta));
            }
            catch (Exception ex)
            {
                //  Return any exception messages back to the Response header
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.StatusDescription = ex.Message.Replace("\r\n", "");
                return null;
            }
        }

        public wsControl PutCita(Stream JSONdataStream)
        {
            return DBCita.PutCita(JSONdataStream);
        }

        public List<wsMaestro> GetCitasDia(string IdEmpresa, string IdBodega, string Fecha)
        {
            return DBCita.GetCitasDia(int.Parse(IdEmpresa), int.Parse(IdBodega), DateTime.Parse(Fecha));
        }

        public List<wsMaestro> GetListasSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(int.Parse(IdEmpresa), "CMGetcot_item_listas");
        }

        public List<wsMaestro> GetDetallePlanesSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(int.Parse(IdEmpresa), "CMGetcot_item_prereq");
        }

        public wsControl PutDetallePlanSync(string IdDetalle)
        {
            return DBItem.PutDetallePlanSincronizar(IdDetalle);
        }

        public List<wsMaestro> GetStockTotal(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(int.Parse(IdEmpresa), "CMGet_Stock");
        }

        public List<wsMaestro> GetUsuarioSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(int.Parse(IdEmpresa), "CMGet_Usuarios");
        }

        public List<wsMaestro> GetSustitutos(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(int.Parse(IdEmpresa), "CMGetcot_item_sus");
        }

        public List<wsMaestro> GetTarifasIva(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(int.Parse(IdEmpresa), "CMGetcot_iva");
        }

        public List<wsVehiculo> GetStockVehiculos(string IdEmpresa, string IdBodega)
        {
            try
            {
                return DBVehiculos.GetStockVehiculos(IdEmpresa, IdBodega);
            }
            catch (Exception ex)
            {
                //  Return any exception messages back to the Response header
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.StatusDescription = ex.Message.Replace("\r\n", "");
                return null;
            }
        }

        public wsControl putEstadoOperacion(Stream JSONdataStream)
        {
            try
            {
                return DBOrden.putEstadoOperacion(JSONdataStream);
            }
            catch (Exception ex)
            {
                //  Return any exception messages back to the Response header
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.StatusDescription = ex.Message.Replace("\r\n", "");
                return null;
            }
        }

        public List<wsMaestro> GetOrigenesSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(int.Parse(IdEmpresa), "CMGetcot_cliente_origenes");
        }

        public wsControl putEjecucionOperacion(Stream JSONdataStream)
        {
            try
            {
                return DBOrden.putEjecucionOperacion(JSONdataStream);
            }
            catch (Exception ex)
            {
                //  Return any exception messages back to the Response header
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.StatusDescription = ex.Message.Replace("\r\n", "");
                return null;
            }
        }

        public List<wsPedido> GetPedidosCliente(string IdCliente, string FechaDesde, string FechaHasta)
        {
           
            try
            {
                return DBcliente.GetPedidos(int.Parse(IdCliente), DateTime.Parse(FechaDesde), DateTime.Parse(FechaHasta));

            }
            catch (Exception ex)
            {
                //  Return any exception messages back to the Response header
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.StatusDescription = ex.Message.Replace("\r\n", "");
                return null;
            }
           
        }

        public wsPedido GetPedido(string IdPedido)
        {
           
            try
            {
                return DBPedido.GetPedido(int.Parse(IdPedido));

            }
            catch (Exception ex)
            {
                //  Return any exception messages back to the Response header
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.StatusDescription = ex.Message.Replace("\r\n", "");
                return null;
            }
            
        }

        public wsOrdenTaller GetOrden(string IdOrden)
        {
            try
            {
                return DBOrden.GetOrden(int.Parse(IdOrden));

            }
            catch (Exception ex)
            {
                //  Return any exception messages back to the Response header
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.StatusDescription = ex.Message.Replace("\r\n", "");
                return null;
            }
        }
              
        public wsControl putVehiculo(Stream JSONdataStream)
        {
            return DBVehiculos.PutVehiculo(JSONdataStream);
        }

        public List<wsMaestro> GetInventarioUsados(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(int.Parse(IdEmpresa), "CMGet_stockusados");
        }

        public List<wsMaestro> GetVehiculosSalenDias(string IdEmpresa, string FechaDesde, string FechaHasta)
        {           
            return DBCita.GetVehiculosSalenFechas(int.Parse(IdEmpresa), DateTime.Parse(FechaDesde), DateTime.Parse(FechaHasta));
        }

        public List<wsMaestro> GetEstadisticaVehiculo(string IdVehiculo, string FechaDesde, string FechaHasta)
        {
            try
            {
                return DBVehiculos.GetEstadistica(int.Parse(IdVehiculo), DateTime.Parse(FechaDesde), DateTime.Parse(FechaHasta));
            }
            catch (Exception ex)
            {
                //  Return any exception messages back to the Response header
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.StatusDescription = ex.Message.Replace("\r\n", "");
                return null;
            }
        }

        public List<wsMaestro> GetStatusHn(string Id)
        {
            try
            {
                return DBVehiculos.GetStatusHn(int.Parse(Id));
            }
            catch (Exception ex)
            {
                //  Return any exception messages back to the Response header
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.StatusDescription = ex.Message.Replace("\r\n", "");
                return null;
            }
        }

        public wsControl putItemOrden(Stream JSONdataStream)
        {
            try
            {
                return DBOrden.putItemOrden(JSONdataStream);
            }
            catch (Exception ex)
            {
                //  Return any exception messages back to the Response header
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.StatusDescription = ex.Message.Replace("\r\n", "");
                return null;
            }
        }

        public wsControl delItemOrden(string Id)
        {
            try
            {
                return DBOrden.DelItemOrden(int.Parse(Id));
            }
            catch (Exception ex)
            {
                //  Return any exception messages back to the Response header
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.StatusDescription = ex.Message.Replace("\r\n", "");
                return null;
            }
        }

        public wsControl delItemPedido(string Id)
        {
            try
            {
                return DBPedido.DelItemPedido(int.Parse(Id));
            }
            catch (Exception ex)
            {
                //  Return any exception messages back to the Response header
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.StatusDescription = ex.Message.Replace("\r\n", "");
                return null;
            }
        }

        public List<wsMaestro> getSgSync(string IdEmpresa)
        {   
            return DBMaestro.GetMaestro(int.Parse(IdEmpresa), "CMGetcot_grupo_subgrupos3");
        }

        public ResumenCartera GetResumenCartera(string Nit)
        {
            return DBcliente.GetResumenCartera(Nit);
        }

        public List<DocumentoCartera> GetDetalleCartera(string Nit)
        {
            return DBcliente.GetDetalleCartera(Nit);
        }
        
          public wsMaestro Get_Stock2(string IdItem, string IdBodega)
        {
            return DBItem.GetStock2(int.Parse(IdItem), int.Parse(IdBodega));
        }
    }
}

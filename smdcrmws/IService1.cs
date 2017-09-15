using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using smdcrmws.dto;
using System.IO;

namespace smdcrmws
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ISmdcrmws
    {       

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);
                
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getClientesSync/{IdEmpresa}")]
        List<wsCliente> GetClientesSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "MarcarClienteSync/{IdCliente}")]
        wsControl MarcarClienteSync(string IdCliente);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "putClienteSync")]
        wsControl PutClienteSync(Stream JSONdataStream);

       

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "putClientesSync")]
        wsControl PutClientesSync(Stream JSONdataStream);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getOperariosSync/{IdEmpresa}")]
        List<wsOperario> GetOperariosSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getItemsSync/{IdEmpresa}")]
        List<wsItem> GetItemsSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "MarcarItemSync/{IdItem}")]
        wsControl MarcarItemSync(string IdItem);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "putItemsSync")]
        wsControl PutItemsSync(Stream JSONdataStream);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getTiposTributariosSync/{IdEmpresa}")]
        List<wsMaestro> GetTiposTributariosSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getPaisesSync/{IdEmpresa}")]
        List<wsMaestro> GetPaisesSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getBarriosSync/{IdEmpresa}")]
        List<wsMaestro> GetBarriosSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getEstadosClienteSync/{IdEmpresa}")]
        List<wsMaestro> GetEstadosClienteSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getGruposSync/{IdEmpresa}")]
        List<wsMaestro> GetGruposSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getSubgruposSync/{IdEmpresa}")]
        List<wsMaestro> GetSubgruposSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getBodegasSync/{IdEmpresa}")]
        List<wsMaestro> GetBodegasSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getTallasSync/{IdEmpresa}")]
        List<wsMaestro> GetTallasSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getColoresSync/{IdEmpresa}")]
        List<wsMaestro> GetColoresSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getCategoriasSync/{IdEmpresa}")]
        List<wsMaestro> GetCategoriasSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getUnidadesSync/{IdEmpresa}")]
        List<wsMaestro> GetUnidadesSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getContactosSync/{IdEmpresa}")]
        List<wsContacto> GetContactosSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getBancosSync/{IdEmpresa}")]
        List<wsMaestro> GetBancosSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getProfesionesSync/{IdEmpresa}")]
        List<wsMaestro> GetProfesionesSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getCargosSync/{IdEmpresa}")]
        List<wsMaestro> GetCargosSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "MarcarContactoSync/{IdContacto}")]
        wsControl MarcarContactoSync(string IdContacto);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getActividadesSync/{IdEmpresa}")]
        List<wsMaestro> GetActividadesSync(string IdEmpresa);
        
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getFormasPagoSync/{IdEmpresa}")]
        List<wsMaestro> GetFormasPagoSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getTiposClienteSync/{IdEmpresa}")]
        List<wsMaestro> GetTiposClienteSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getPerfilesClienteSync/{IdEmpresa}")]
        List<wsMaestro> GetPerfilesClienteSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getSubgrupos3Sync/{IdEmpresa}")]
        List<wsMaestro> GetSubgrupos3Sync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getSubgrupos4Sync/{IdEmpresa}")]
        List<wsMaestro> GetSubgrupos4Sync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getSubgrupos5Sync/{IdEmpresa}")]
        List<wsMaestro> GetSubgrupos5Sync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getZonasSync/{IdEmpresa}")]
        List<wsMaestro> GetZonasSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getSubzonasSync/{IdEmpresa}")]
        List<wsMaestro> GetSubzonasSync(string IdEmpresa);

        //vehículos

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getMarcasVehSync/{IdEmpresa}")]
        List<wsMaestro> GetMarcasVehSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getLineasVehSync/{IdEmpresa}")]
        List<wsMaestro> GetLineasVehSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getClasesVehSync/{IdEmpresa}")]
        List<wsMaestro> GetClasesVehSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getTiposServicioVehSync/{IdEmpresa}")]
        List<wsMaestro> GetTiposServicioVehSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getModelosLineaVehSync/{IdEmpresa}")]
        List<wsLineaModelo> GetModelosLineaVehSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "MarcarModeloLineaSync/{IdModeloLinea}")]
        wsControl MarcarModeloLineaSync(string IdModeloLinea);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getModelosAnoSync/{IdEmpresa}")]
        List<wsItem> GetModelosAnoSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "putEncabezadoHn")]
        wsControl PutEncabezadoHn(Stream JSONdataStream);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "putCotizacion")]
        wsControl PutCotizacion(Stream JSONdataStream);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "putCliente")]
        wsControl PutCliente(Stream JSONdataStream);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "putCotCliente")]
        wsControl PutCotCliente(Stream JSONdataStream);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getVehiculosSync/{IdEmpresa}")]
        List<wsVehiculo> GetVehiculosSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "MarcarVehiculoSync/{IdVehiculo}")]
        wsControl MarcarVehiculoSync(string IdVehiculo);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "putOrdenTaller")]
        wsControl PutOrdenTaller(Stream JSONdataStream);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getMotivosIngresoSync/{IdEmpresa}")]
        List<wsMaestro> GetMotivosIngresoSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "putPedido")]
        wsControl PutPedido(Stream JSONdataStream);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getStock/{IdItem}/{IdBodega}")]
        wsStock GetStock(string IdItem, string IdBodega);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "GetPlanesSync/{IdEmpresa}")]
        List<wsMaestro> GetPlanesSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "GetCampsSync/{IdEmpresa}")]
        List<wsMaestro> GetCampsSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "MarcarCampSync/{IdCamp}")]
        wsControl MarcarCampSync(string IdCamp);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "GetEstadisticaCliente/{IdCliente}/{FechaDesde}/{FechaHasta}")]
        List<wsMaestro> GetEstadisticaCliente(string IdCliente, string FechaDesde, string FechaHasta);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "putCita")]
        wsControl PutCita(Stream JSONdataStream);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "GetCitasDia/{IdEmpresa}/{IdBodega}/{Fecha}")]
        List<wsMaestro> GetCitasDia(string IdEmpresa, string IdBodega, string Fecha);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "GetListasSync/{IdEmpresa}")]
        List<wsMaestro> GetListasSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "GetDetallePlanesSync/{IdEmpresa}")]
        List<wsMaestro> GetDetallePlanesSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "PutDetallePlanSync/{IdDetalle}")]
        wsControl PutDetallePlanSync(string IdDetalle);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "GetStockTotal/{IdEmpresa}")]
        List<wsMaestro> GetStockTotal(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "GetUsuariosSync/{IdEmpresa}")]
        List<wsMaestro> GetUsuarioSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "GetSustitutos/{IdEmpresa}")]
        List<wsMaestro> GetSustitutos(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "GetTarifasIva/{IdEmpresa}")]
        List<wsMaestro> GetTarifasIva(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "GetStockVehiculos/{IdEmpresa}/{IdBodega}")]
        List<wsVehiculo> GetStockVehiculos(string IdEmpresa, string IdBodega);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "putEstadoOperacion")]
        wsControl putEstadoOperacion(Stream JSONdataStream);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getOrigenesSync/{IdEmpresa}")]
        List<wsMaestro> GetOrigenesSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "putEjecucionOperacion")]
        wsControl putEjecucionOperacion(Stream JSONdataStream);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getPedidosCliente/{IdCliente}/{FechaDesde}/{FechaHasta}")]
        List<wsPedido> GetPedidosCliente(string IdCliente, string FechaDesde, string FechaHasta);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getPedido/{IdPedido}")]
        wsPedido GetPedido(string IdPedido);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getOrden/{IdOrden}")]
        wsOrdenTaller GetOrden(string IdOrden);

    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}

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

        //[OperationContract]
        //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "putClientesSync")]
        //int PutClientesSync(Stream JSONdataStream);

        //getOrdersForCustomer/{customerID}
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

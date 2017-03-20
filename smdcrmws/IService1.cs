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
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getData/{value}")]
        string GetData(string value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);
                
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getClientesSync/{IDEmpresa}")]
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
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getOperariosSync/{IDEmpresa}")]
        List<wsOperario> GetOperariosSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getItemsSync/{IDEmpresa}")]
        List<wsItem> GetItemsSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "MarcarItemSync/{IdItem}")]
        wsControl MarcarItemSync(string IdItem);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "putItemsSync")]
        wsControl PutItemsSync(Stream JSONdataStream);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getTiposTributariosSync/{IDEmpresa}")]
        List<wsMaestro> GetTiposTributariosSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getPaisesSync/{IDEmpresa}")]
        List<wsMaestro> GetPaisesSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getBarriosSync/{IDEmpresa}")]
        List<wsMaestro> GetBarriosSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getEstadosClienteSync/{IDEmpresa}")]
        List<wsMaestro> GetEstadosClienteSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getGruposSync/{IDEmpresa}")]
        List<wsMaestro> GetGruposSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getSubgruposSync/{IDEmpresa}")]
        List<wsMaestro> GetSubgruposSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getBodegasSync/{IDEmpresa}")]
        List<wsMaestro> GetBodegasSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getTallasSync/{IDEmpresa}")]
        List<wsMaestro> GetTallasSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getColoresSync/{IDEmpresa}")]
        List<wsMaestro> GetColoresSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getCategoriasSync/{IDEmpresa}")]
        List<wsMaestro> GetCategoriasSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getUnidadesSync/{IDEmpresa}")]
        List<wsMaestro> GetUnidadesSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getContactosSync/{IDEmpresa}")]
        List<wsContacto> GetContactosSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getBancosSync/{IDEmpresa}")]
        List<wsMaestro> GetBancosSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getProfesionesSync/{IDEmpresa}")]
        List<wsMaestro> GetProfesionesSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getCargosSync/{IDEmpresa}")]
        List<wsMaestro> GetCargosSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "MarcarContactoSync/{IdContacto}")]
        wsControl MarcarContactoSync(string IdContacto);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getActividadesSync/{IDEmpresa}")]
        List<wsMaestro> GetActividadesSync(string IdEmpresa);
        
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getFormasPagoSync/{IDEmpresa}")]
        List<wsMaestro> GetFormasPagoSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getTiposClienteSync/{IDEmpresa}")]
        List<wsMaestro> GetTiposClienteSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getPerfilesClienteSync/{IDEmpresa}")]
        List<wsMaestro> GetPerfilesClienteSync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getSubgrupos3Sync/{IDEmpresa}")]
        List<wsMaestro> GetSubgrupos3Sync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getSubgrupos4Sync/{IDEmpresa}")]
        List<wsMaestro> GetSubgrupos4Sync(string IdEmpresa);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getSubgrupos5Sync/{IDEmpresa}")]
        List<wsMaestro> GetSubgrupos5Sync(string IdEmpresa);

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

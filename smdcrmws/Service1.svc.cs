using System;
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
        public string GetData(string value)
        {
            return string.Format("You entered: {0}", value);
        }

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

        //public wsControl PutClientesSync(Stream JSONdataStream)
        //{
        //    return DBcliente.PutClientesSincronizar(JSONdataStream);
        //}

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
                return DBItem.GetItemsSincronizar(IdEmpresa);

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
            return DBMaestro.GetMaestro(IdEmpresa, "CMGettipo_tributarios");
        }

        public List<wsMaestro> GetPaisesSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(IdEmpresa, "CMGetcot_cliente_paises");
        }

        public List<wsMaestro> GetBarriosSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(IdEmpresa, "CMGetcot_cliente_barrios");
        }

        public List<wsMaestro> GetEstadosClienteSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(IdEmpresa, "CMGetcot_estados");
        }

        public List<wsMaestro> GetGruposSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(IdEmpresa, "CMGetcot_grupos");
        }

        public List<wsMaestro> GetSubgruposSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(IdEmpresa, "CMGetcot_grupo_subs");
        }

        public List<wsMaestro> GetBodegasSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(IdEmpresa, "CMGetcot_bodegas");
        }

        public List<wsMaestro> GetTallasSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(IdEmpresa, "CMGetcot_item_tallas");
        }

        public List<wsMaestro> GetColoresSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(IdEmpresa, "CMGetcot_item_tallas");
        }

        public List<wsMaestro> GetCategoriasSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(IdEmpresa, "CMGetcot_item_categorias");
        }

        public List<wsMaestro> GetUnidadesSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(IdEmpresa, "CMGetcot_unidades");
        }

        public wsControl MarcarClienteSync(string IdCliente)
        {
            return DBcliente.MarcarClienteSincronizar(IdCliente);
        }

        public wsControl MarcarItemSync(string IdItem)
        {
            return DBItem.MarcarItemSincronizar(int.Parse(IdItem));
        }

        public List<wsContacto> GetContactosSync(string IdEmpresa)
        {
            return DBcliente.GetContactosSincronizar(IdEmpresa);
        }

        public List<wsMaestro> GetBancosSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(IdEmpresa, "CMGettes_bancos");
        }

        public List<wsMaestro> GetProfesionesSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(IdEmpresa, "CMGetcot_profesiones");            
        }

        public List<wsMaestro> GetCargosSync(string IdEmpresa)
        {            
            return DBMaestro.GetMaestro(IdEmpresa, "CMGetcot_cliente_cargos");
        }

        public wsControl MarcarContactoSync(string IdContacto)
        {
            return DBcliente.MarcarContactoSincronizar(IdContacto);
        }

        public List<wsMaestro> GetActividadesSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(IdEmpresa, "CMGetcot_cliente_actividades");
        }

        public List<wsMaestro> GetFormasPagoSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(IdEmpresa, "CMGetcot_formas_pago");
        }

        public List<wsMaestro> GetTiposClienteSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(IdEmpresa, "CMGetcot_cliente_tipos");
        }

        public List<wsMaestro> GetPerfilesClienteSync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(IdEmpresa, "CMGetcot_cliente_perfiles");
        }

        public List<wsMaestro> GetSubgrupos3Sync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(IdEmpresa, "CMGetcot_grupo_subgrupos3");
        }

        public List<wsMaestro> GetSubgrupos4Sync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(IdEmpresa, "CMGetcot_grupo_subgrupos4");
        }

        public List<wsMaestro> GetSubgrupos5Sync(string IdEmpresa)
        {
            return DBMaestro.GetMaestro(IdEmpresa, "CMGetcot_grupo_subgrupos5");
        }
    }
}

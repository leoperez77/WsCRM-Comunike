using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
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
    }
}

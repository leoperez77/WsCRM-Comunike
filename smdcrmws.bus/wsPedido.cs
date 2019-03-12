using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Reflection;

namespace smdcrmws.dto
{
    [DataContract]
    [Serializable]
    public class wsPedido
    {
        [DataMember]
        public String Id { get; set; }

        [DataMember]
        public String IdEmpresa { get; set; }

        [DataMember]
        public String Usuario { get; set; }

        [DataMember]
        public String Bodega { get; set; }

        [DataMember]
        public String Fecha { get; set; }

        [DataMember]
        public String TipoDocumento { get; set; }

        [DataMember]
        public String Cliente { get; set; }

        [DataMember]
        public String Vendedor { get; set; }

        [DataMember]
        public String FormaPago { get; set; }

        [DataMember]
        public String Contacto { get; set; }

        [DataMember]
        public String Dias { get; set; }

        [DataMember]
        public String Subtotal { get; set; }

        [DataMember]
        public String Descuento { get; set; }

        [DataMember]
        public String Iva { get; set; }

        [DataMember]
        public String Total { get; set; }

        [DataMember]
        public String Estado { get; set; }

        [DataMember]
        public String Moneda { get; set; }

        [DataMember]
        public String Tasa { get; set; }

        [DataMember]
        public String FechaEstimada { get; set; }

        [DataMember]
        public String Factibilidad { get; set; }

        [DataMember]
        public String NotasInternas { get; set; }

        [DataMember]
        public String Notas { get; set; }

        [DataMember]
        public String TipoReferencia { get; set; }

        [DataMember]
        public String NumeroReferencia { get; set; }

        [DataMember]
        public String IdOrden { get; set; }

        [DataMember]
        public String Numero { get; set; }

        [DataMember]
        public String Origen { get; set; }

        [DataMember]
        public List<wsDetallePedido> Detalle = new List<wsDetallePedido>();
                
    }
    
    public class wsDetallePedido
    {
        [DataMember]
        public String Id { get; set; }

        [DataMember]
        public String Renglon { get; set; }

        [DataMember]
        public String IdCotizacion { get; set; }

        [DataMember]
        public String IdItem { get; set; }

        [DataMember]
        public String Cantidad { get; set; }

        [DataMember]
        public String Precio { get; set; }

        [DataMember]
        public String PrecioCotizado { get; set; }

        [DataMember]
        public String Iva { get; set; }

        [DataMember]
        public String Notas { get; set; }

        [DataMember]
        public String Subtotal { get; set; }

        [DataMember]
        public String Descuento { get; set; }

        [DataMember]
        public String Und { get; set; }

        [DataMember]
        public String Conversion { get; set; }

        [DataMember]
        public String IdLote { get; set; }

        [DataMember]
        public String Facturar { get; set; }

    }    
}

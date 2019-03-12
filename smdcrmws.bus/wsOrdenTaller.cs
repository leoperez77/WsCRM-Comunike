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
    public class wsOrdenTaller
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
        public String Contacto { get; set; }

        [DataMember]
        public String Subtotal { get; set; }

        [DataMember]
        public String Descuento { get; set; }

        [DataMember]
        public String Iva { get; set; }

        [DataMember]
        public String Total { get; set; }

        [DataMember]
        public String FechaEstimada { get; set; }

        [DataMember]
        public String NotasInternas { get; set; }

        [DataMember]
        public String Notas { get; set; }

        [DataMember]
        public String Kilometraje { get; set; }

        [DataMember]
        public String IdLote { get; set; }

        [DataMember]
        public String Rombo { get; set; }
        
        [DataMember]
        public String TipoReferencia { get; set; }

        [DataMember]
        public String NumeroReferencia { get; set; }

        [DataMember]
        public String Aseguradora { get; set; }

        [DataMember]
        public String Deducible { get; set; }

        [DataMember]
        public String MinimoDeducible { get; set; }
                
        [DataMember]
        public String IdMotivo { get; set; }
                
        [DataMember]
        public String ValorHora { get; set; }
        
        [DataMember]
        public String IdPlan { get; set; }

        [DataMember]
        public String IdCita { get; set; }

        [DataMember]
        public String Numero { get; set; }

        [DataMember]
        public String Origen { get; set; }

        [DataMember]
        public List<wsOperacionOrden> Detalle = new List<wsOperacionOrden>();
    }
        
    public class wsOperacionOrden
    {
        [DataMember]
        public String Id { get; set; }

        [DataMember]
        public String IdCotizacion { get; set; }

        [DataMember]
        public String IdItem { get; set; }

        [DataMember]
        public String Cantidad { get; set; }

        [DataMember]
        public String Tiempo { get; set; }

        [DataMember]
        public String ValorHora { get; set; }

        [DataMember]
        public String ValorOperacion { get; set; }
        
        [DataMember]
        public String Renglon { get; set; }

        [DataMember]
        public String Iva { get; set; }

        [DataMember]
        public String Notas { get; set; }
        
        [DataMember]
        public String PorcentajeDescuento { get; set; }
        
        [DataMember]
        public String Operario { get; set; }

        [DataMember]
        public String Facturar { get; set; }

        [DataMember]
        public String TipoOperacion { get; set; }               

       

    }
}


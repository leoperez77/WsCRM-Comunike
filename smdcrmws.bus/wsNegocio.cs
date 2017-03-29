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
    public class wsNegocio
    {
        [DataMember]
        public String Id { get; set; }

        [DataMember]
        public String IdEmpresa { get; set; }

        [DataMember]
        public String Fecha { get; set; }

        [DataMember]
        public String Estado { get; set; }

        [DataMember]
        public String TipoDocumento { get; set; }

        [DataMember]
        public String Bodega { get; set; }

        [DataMember]
        public String Contacto { get; set; }

        [DataMember]
        public String Usuario { get; set; }

        [DataMember]
        public String Vendedor { get; set; }

        [DataMember]
        public String UsuarioRevisa { get; set; }

        [DataMember]
        public String UsuarioAprueba { get; set; }

        [DataMember]
        public String FechaEntrega { get; set; }

        [DataMember]
        public String DocReferencia { get; set; }

        [DataMember]
        public String Notas { get; set; }

        [DataMember]
        public String NotasRevision { get; set; }

        [DataMember]
        public String NotasAprobacion { get; set; }
    }
}

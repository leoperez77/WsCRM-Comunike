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

 //   @IdRetorno int output,
 //   @id_emp int,
	//@id int,
	//@fecha datetime,
 //   @estado smallint,
	//@id_cot_tipo int,
	//@id_cot_bodega int,
	//@id_cot_cliente_contacto int,
	//@id_usuario int,
	//@id_usuario_vende int,
	//@id_usuario_rev int,
	//@id_usuario_aprobo int,
	//@FechaEntrega datetime,
 //   @doc_ref varchar(20),
	//@notas_reviso varchar(250),
	//@notas_aprobo varchar(250),
	//@notas text
}

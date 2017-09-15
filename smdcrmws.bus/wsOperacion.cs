using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smdcrmws.dto
{
    [DataContract]
    [Serializable]
    public class wsOperacion
    {
        [DataMember]
        public String IdLineaOperacion { get; set; }

        [DataMember]
        public String IdUsuario { get; set; }

        [DataMember]
        public String Estado { get; set; }

        [DataMember]
        public String IdCentro { get; set; }
    }
}

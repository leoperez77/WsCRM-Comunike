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
    public class wsCita
    {
        [DataMember]
        public String Id { get; set; }

        [DataMember]
        public String IdEmpresa { get; set; }

        [DataMember]
        public String Bodega { get; set; }

        [DataMember]
        public String IdVehiculo { get; set; }

        [DataMember]
        public String IdPlan { get; set; }

        [DataMember]
        public String IdCamp { get; set; }

        [DataMember]
        public String Hora { get; set; }

        [DataMember]
        public String Responsable { get; set; }

        [DataMember]
        public String Telefono { get; set; }

        [DataMember]
        public String Notas { get; set; }
               
    }
}

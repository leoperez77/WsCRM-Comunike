using System;
using System.Runtime.Serialization;
using System.Reflection;
using System.Collections.Generic;
namespace smdcrmws.dto
{
    [DataContract]
    [Serializable]
    public class wsStock
    {
        [DataMember]
        public String IdItem { get; set; }
              
        [DataMember]
        public String Stock { get; set; }

        [DataMember]
        public String Pendiente { get; set; }               

        [DataMember]
        public String Und { get; set; }

        [DataMember]
        public String CanUnd { get; set; }

        [DataMember]
        public List<wsDetalleStock> Detalle = new List<wsDetalleStock>();
    }

    public class wsDetalleStock
    {
        [DataMember]
        public String IdLote { get; set; }

        [DataMember]
        public String Lote { get; set; }

        [DataMember]
        public String Stock { get; set; }

        [DataMember]
        public String Pendiente { get; set; }

        [DataMember]
        public String Vencimiento { get; set; }

        [DataMember]
        public String Transito { get; set; }

    }
}

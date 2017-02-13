using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace sdmcrmws.data
{
    [Serializable]
    [DataContract]
    public class wsControl 
    {

        [DataMember]
        public string Estado { get; set; }

        [DataMember]
        public string Origen { get; set; }

        [DataMember]
        public string Error { get; set; }

        [DataMember]
        public string FechaHora { get; set; }
     
    }
}

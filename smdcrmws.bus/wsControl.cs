using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Reflection;
namespace smdcrmws.dto
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

        [DataMember]
        public string IdGenerado { get; set; }

        public object this[string propertyName]
        {
            get
            {
                PropertyInfo property = GetType().GetProperty(propertyName);
                return property.GetValue(this, null);
            }
            set
            {
                PropertyInfo property = GetType().GetProperty(propertyName);
                property.SetValue(this, value, null);
            }
        }

    }
}

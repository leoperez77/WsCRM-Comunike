using System;
using System.Runtime.Serialization;
using System.Reflection;
namespace smdcrmws.dto
{
    [DataContract]
    [Serializable]
    public class wsMaestro
    {
        [DataMember]
        public String Campo_1 { get; set; }

        [DataMember]
        public String Campo_2 { get; set; }

        [DataMember]
        public String Campo_3 { get; set; }

        [DataMember]
        public String Campo_4 { get; set; }

        [DataMember]
        public String Campo_5 { get; set; }

        [DataMember]
        public String Campo_6 { get; set; }

        [DataMember]
        public String Campo_7 { get; set; }

        [DataMember]
        public String Campo_8 { get; set; }

        [DataMember]
        public String Campo_9 { get; set; }

        [DataMember]
        public String Campo_10 { get; set; }

        [DataMember]
        public String Campo_11 { get; set; }

        [DataMember]
        public String Campo_12{ get; set; }

        [DataMember]
        public String Campo_13 { get; set; }

        [DataMember]
        public String Campo_14 { get; set; }
        
        [DataMember]
        public String Campo_15 { get; set; }


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

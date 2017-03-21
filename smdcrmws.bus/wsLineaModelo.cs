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
    public class wsLineaModelo
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
        public String Campo_12 { get; set; }

        [DataMember]
        public String Campo_13 { get; set; }

        [DataMember]
        public String Campo_14 { get; set; }

        [DataMember]
        public String Campo_15 { get; set; }

        [DataMember]
        public String Campo_16 { get; set; }

        [DataMember]
        public String Campo_17 { get; set; }

        [DataMember]
        public String Campo_18 { get; set; }

        [DataMember]
        public String Campo_19 { get; set; }

        [DataMember]
        public String Campo_20 { get; set; }

        [DataMember]
        public String Campo_21 { get; set; }

        [DataMember]
        public String Campo_22 { get; set; }

        [DataMember]
        public String Campo_23 { get; set; }

        [DataMember]
        public String Campo_24 { get; set; }

        [DataMember]
        public String Campo_25 { get; set; }

        [DataMember]
        public String Campo_26 { get; set; }

        [DataMember]
        public String Campo_27 { get; set; }

        [DataMember]
        public String Campo_28 { get; set; }

        [DataMember]
        public String Campo_29 { get; set; }

        [DataMember]
        public String Campo_30 { get; set; }

        [DataMember]
        public String Campo_31 { get; set; }

        [DataMember]
        public String Campo_32 { get; set; }

        [DataMember]
        public String Campo_33 { get; set; }

        [DataMember]
        public String Campo_34 { get; set; }

        [DataMember]
        public String Campo_35 { get; set; }

        [DataMember]
        public String Campo_36 { get; set; }

        [DataMember]
        public String Campo_37 { get; set; }

        [DataMember]
        public String Campo_38 { get; set; }

        [DataMember]
        public String Campo_39 { get; set; }

        [DataMember]
        public String Campo_40 { get; set; }

        [DataMember]
        public String Campo_41 { get; set; }

        [DataMember]
        public String Campo_42 { get; set; }

        [DataMember]
        public String Campo_43 { get; set; }

        [DataMember]
        public String Campo_44 { get; set; }

        [DataMember]
        public String Campo_45 { get; set; }

        [DataMember]
        public String Campo_46 { get; set; }

        [DataMember]
        public String Campo_47 { get; set; }

        [DataMember]
        public String Campo_48 { get; set; }

        [DataMember]
        public String Campo_49 { get; set; }

        [DataMember]
        public String Campo_50 { get; set; }

        [DataMember]
        public String Campo_51 { get; set; }

        [DataMember]
        public String Campo_52 { get; set; }

        [DataMember]
        public String Campo_53 { get; set; }

        [DataMember]
        public String Campo_54 { get; set; }

        [DataMember]
        public String Campo_55 { get; set; }

        [DataMember]
        public String Campo_56 { get; set; }

        [DataMember]
        public String Campo_57 { get; set; }

        [DataMember]
        public String Campo_58 { get; set; }

        [DataMember]
        public String Campo_59 { get; set; }

        [DataMember]
        public String Campo_60 { get; set; }

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

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace sdmcrmws.data
{

    public static class MapDataReader
    {
        public static List<T> DataReaderMapToList<T>(IDataReader dr)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    try
                    {
                        if (!object.Equals(dr[prop.Name], DBNull.Value))
                        {
                            prop.SetValue(obj, dr[prop.Name], null);
                        }
                    }
                    catch 
                    {
                        prop.SetValue(obj, null);
                    }
                }
                list.Add(obj);
            }
            dr.Close();
            return list;
        }

        public static T MapToObject<T>(IDataReader dr)
        {
            //List<T> list = new List<T>();
            T obj = default(T);
            if (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    try
                    {
                        if (!object.Equals(dr[prop.Name], DBNull.Value))
                        {
                            prop.SetValue(obj, dr[prop.Name], null);
                        }
                    }
                    catch 
                    {
                        prop.SetValue(obj, null);
                    }

                }
            }
            dr.Close();
            return obj;
        }
    }
}
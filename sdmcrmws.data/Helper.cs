using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sdmcrmws
{
    public static class Helper
    {
     
        internal static int Bcero(string x)
        {
            if (x == "")
                return 0;
            else
                return int.Parse(x);
        }
    }
}
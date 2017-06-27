using NFEXL.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NFEXL.Extension
{
    public static class PropertyExtension
    {
        public static PropertyInfo GetPropertyByOrder( this Type type, int order)
        {
            return type.GetProperties().Where(prp => ((XLAttribute)prp.GetCustomAttributes(true)[0]).Order.Equals(order)).FirstOrDefault(); ;
        }

        public static XLAttribute GetXLAttribute(this PropertyInfo prop)
        {
            return ((XLAttribute)prop.GetCustomAttributes(true)[0]);
        }
    }
}


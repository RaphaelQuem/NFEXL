using System;
namespace NFEXL.Extension
{
    public static class StringExtension
    {

        public static T ToNumericType<T>(this string text)
        {
            if (text.Trim().Equals(""))
               return default(T);       

            return (T)Convert.ChangeType(text, typeof(T)); 
        }

    }
}

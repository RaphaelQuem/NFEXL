using System;

namespace NFEXL.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class XLAttribute : Attribute
    {
        public string Name { get; set; }
        public int Order { get; set; }
    }
}

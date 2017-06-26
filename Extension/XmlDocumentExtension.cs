using NFEXL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NFEXL.Extension
{
    public static class XmlDocumentExtension
    {
        public static IFiscalDocument ToFiscalDocument(this XmlDocument doc)
        {
                return new CFe(doc);
        }
    }
}

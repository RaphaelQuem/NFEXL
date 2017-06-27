using NFEXL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NFEXL.Extension
{
    public static class XmlExtension
    {
        public static IFiscalDocument ToFiscalDocument(this XmlDocument doc)
        {
            return new CFe(doc);
        }
        public static IFiscalDocumentItem ToFiscalDocumentItem(this XmlNode nod)
        {
            return new CFeItem(nod);
        }
        public static string GetNodeValue(this XmlDocument doc, string node, string fathernode = "")
        {
            string value = "";
            if (!fathernode.Equals(""))
            {
                try
                {
                    value = doc.GetElementsByTagName(fathernode)[0][node].InnerText;
                }
                catch
                {

                }
            }
            else
            {
                try
                {
                    value = doc.GetElementsByTagName(node)[0].InnerText;
                }
                catch
                {

                }
            }
            return value;
        }
        public static string GetNodeValue(this XmlNode nod, string node,string fathernode="")
        {
            string value = "";


            try
            {
                if(!fathernode.Equals(""))
                    value = nod.SelectSingleNode(fathernode).SelectSingleNode(node).InnerText;
                else
                    value = nod.SelectSingleNode(node).InnerText;

            }
            catch
            {

            }

            return value;
        }


    }
}

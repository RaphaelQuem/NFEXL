using NFEXL.Interface;
using NFEXL.Model;
using System;
using System.Xml;

namespace NFEXL.Extension
{
    public  enum ItemType
    {
        NfeItemType,
        CfeItemType,
    }
    public static class XmlExtension
    {
        public static IFiscalDocument ToFiscalDocument(this XmlDocument doc)
        {
            if (doc.GetElementsByTagName("infNFe").Count > 0)
                return new NFe(doc);
            else if (doc.GetElementsByTagName("infCFe").Count > 0)
                return new CFe(doc);
            else
                return new IDEvent(doc);
        }
        public static IFiscalDocumentItem ToFiscalDocumentItem(this XmlNode nod,ItemType type)
        {
            if (type.Equals(ItemType.NfeItemType))
                return new NFeItem(nod);
            else
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
        public static string GetNodeByPath(this XmlNode nod, string path)
        {

            int slash =path.IndexOf("/");
            string relativepath = (slash > 0? path.Substring(0, path.IndexOf("/")):path);
            string pathtail = (slash > 0 ? path.Replace(relativepath + "/", "") : "");


            foreach (XmlNode childnod  in nod.ChildNodes)
            {
                if(childnod.Name.Equals(relativepath))
                {
                    if (path.Equals(relativepath))
                        return childnod.InnerText;
                    else
                        return childnod.GetNodeByPath(pathtail);
                }
            }


            return "";
        }


    }
}

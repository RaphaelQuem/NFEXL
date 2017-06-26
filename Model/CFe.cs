using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NFEXL.Model
{
    public class CFe : IFiscalDocument
    {
        public string ClientCode
        {
            get;
            set;
        }
        public string ClientName
        {
            get;
            set;
        }
        public string CompanyCode
        {
            get;
            set;
        }
        public string CompanyName { get; set; }
        public uint DocumentNumber
        {
            get;
            set;
        }
        public string EmissionDate
        {
            get;
            set;
        }
        public List<IFiscalDocumentItem> Items
        {
            get;
            set;
        }
        public string Key
        {
            get;
            set;
        }
        public string Mod
        {
            get;
            set;
        }
        public string State
        {
            get;
            set;
        }
        public double TotalDiscount
        {
            get;
            set;
        }
        public double TotalShipping
        {
            get;
            set;
        }
        public double TotalValue
        {
            get;set;
        }

        public CFe(XmlDocument doc)
        {
            string s = doc.BaseURI;

            XmlReader rdr = XmlReader.Create(s);
            XmlNode node = doc.ReadNode(rdr);

            
            Key = doc.GetElementsByTagName("infCFe")[0].Attributes[0].Value;           
            State = doc.GetElementsByTagName("enderDest")[0]["UF"].InnerText;
            Mod = doc.GetElementsByTagName("tpNF")[0]["mod"].InnerText;
            DocumentNumber = Convert.ToUInt32(doc.GetElementsByTagName("nCFe")[0].InnerText);
            EmissionDate = doc.GetElementsByTagName("ide")[0]["dEmi"].InnerText;
            CompanyCode = doc.GetElementsByTagName("emit")[0]["CNPJ"].InnerText;
            CompanyName = doc.GetElementsByTagName("emit")[0]["xNome"].InnerText;
            ClientCode = doc.GetElementsByTagName("dest")[0]["CPF"].InnerText;
            ClientName = doc.GetElementsByTagName("dest")[0]["xNome"].InnerText;
            TotalValue = Convert.ToDouble(doc.GetElementsByTagName("total")[0]["vProd"].InnerText);
            TotalDiscount = Convert.ToDouble(doc.GetElementsByTagName("total")[0]["vDesc"].InnerText);
            TotalShipping = 0;



        }
    }
}

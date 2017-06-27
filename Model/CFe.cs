using NFEXL.Attributes;
using NFEXL.Extension;
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
        [XL(Name = "DATA", Order = 1)]
        public string EmissionDate
        {
            get;
            set;
        }
        [XL(Name = "CHAVE", Order = 2)]
        public string Key
        {
            get;
            set;
        }
        [XL(Name = "NUMERO", Order = 3)]
        public uint DocumentNumber
        {
            get;
            set;
        }
        [XL(Name = "TIPO", Order = 4)]
        public string Mod
        {
            get;
            set;
        }
        [XL(Name = "UF", Order = 5)]
        public string State
        {
            get;
            set;
        }
        [XL(Name = "CPF", Order = 6)]
        public string ClientCode
        {
            get;
            set;
        }
        [XL(Name = "NOME CLI", Order = 7)]
        public string ClientName
        {
            get;
            set;
        }
        [XL(Name = "UF CLI", Order = 8)]
        public string ClientState
        {
            get;
            set;
        }
        [XL(Name = "CNPJ", Order = 9)]
        public string CompanyCode
        {
            get;
            set;
        }
        [XL(Name = "RAZAO", Order = 10)]
        public string CompanyName { get; set; }
        [XL(Name = "ITEMS", Order = 99)]
        public List<IFiscalDocumentItem> Items
        {
            get;
            set;
        }
        [XL(Name = "DESCONTO", Order = 99)]
        public double TotalDiscount
        {
            get;
            set;
        }
        [XL(Name = "FRETE", Order = 99)]
        public double TotalShipping
        {
            get;
            set;
        }
        [XL(Name = "TOTAL", Order = 99)]
        public double TotalValue
        {
            get;set;
        }

        public CFe(XmlDocument doc)
        {

            Key = doc.GetElementsByTagName("infCFe")[0].Attributes[0].Value;
            State = doc.GetNodeValue("cUF");
            Mod = doc.GetNodeValue("mod");
            DocumentNumber = doc.GetNodeValue("nCFe").ToNumericType<uint>();
            EmissionDate = doc.GetNodeValue("dEmi", "ide");
            CompanyCode = doc.GetNodeValue("CNPJ", "emit");
            CompanyName = doc.GetNodeValue("xNome", "emit");
            ClientCode = doc.GetNodeValue("CPF","dest");
            ClientName = doc.GetNodeValue("xNome","dest");
            ClientState = doc.GetNodeValue("UF","enderDest");
            TotalValue = doc.GetNodeValue("vCFe","total").ToNumericType<double>();
            TotalDiscount = doc.GetNodeValue("vDesc", "total").ToNumericType<double>();
            TotalShipping = doc.GetNodeValue("vFrete", "total").ToNumericType<double>(); ;

            Items = new List<IFiscalDocumentItem>();
            XmlNodeList detElements = doc.GetElementsByTagName("det");
            foreach(XmlNode nod in detElements)
            {
                IFiscalDocumentItem item = nod.ToFiscalDocumentItem();
                item.PartialDiscount = 0; //calculo
                item.PartialShipping = 0; //calculo
                Items.Add(item);
            }
            



        }
    }
}

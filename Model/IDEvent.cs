using NFEXL.Attributes;
using NFEXL.Extension;
using NFEXL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NFEXL.Model
{
    public class IDEvent : IFiscalDocument
    {
        [XL(Name = "DATA", Order = 1)]
        public string EmissionDate { get; set; }
        [XL(Name = "CHAVE", Order = 2)]
        public string Key { get; set; }
        [XL(Name = "CNPJ", Order = 3)]
        public string CompanyCode { get; set; }
        [XL(Name = "NUMERO", Order = 99)]
        public uint DocumentNumber { get; set; }
        [XL(Name = "MOD", Order = 99)]
        public string Mod { get; set; }
        [XL(Name = "TIPO", Order = 99)]
        public int Type { get; set; }
        [XL(Name = "UF", Order = 99)]
        public string State { get; set; }
        [XL(Name = "CPF", Order = 99)]
        public string ClientCode { get; set; }
        [XL(Name = "NOME CLI", Order = 99)]
        public string ClientName { get; set; }
        [XL(Name = "UF CLI", Order = 99)]
        public string ClientState { get; set; }

        [XL(Name = "RAZAO", Order = 99)]
        public string CompanyName { get; set; }
        [XL(Name = "ITEMS", Order = 99)]
        public List<IFiscalDocumentItem> Items { get; set; }
        [XL(Name = "DESCONTO", Order = 99)]
        public double TotalDiscount { get; set; }
        [XL(Name = "FRETE", Order = 99)]
        public double TotalShipping { get; set; }
        [XL(Name = "TOTAL", Order = 99)]
        public double TotalValue { get; set; }

        public IDEvent(XmlDocument doc)
        {
            EmissionDate = doc.GetNodeValue("dEmi", "ide");
            Key = doc.GetElementsByTagName("infCFe")[0].Attributes[0].Value;         
            CompanyCode = doc.GetNodeValue("CNPJ", "emit");
        }
    }
}

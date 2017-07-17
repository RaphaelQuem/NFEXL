using NFEXL.Attributes;
using NFEXL.Interface;
using System.Collections.Generic;
using System.Xml;

namespace NFEXL.Model
{
    public class IDEvent : IFiscalDocument
    {
        [XL(Name = "DATA", Order = 1)]
        public string EmissionDate { get; set; }
        [XL(Name = "CHAVE", Order = 2)]
        public string Key { get; set; }
        [XL(Name = "NUMERO", Order = 3)]
        public uint DocumentNumber { get; set; }
        [XL(Name = "MOD", Order = 4)]
        public string Mod { get; set; }
        [XL(Name = "TIPO", Order = 5)]
        public int Type { get; set; }
        [XL(Name = "UF", Order = 6)]
        public string State { get; set; }
        [XL(Name = "CPF", Order = 7)]
        public string ClientCode { get; set; }
        [XL(Name = "NOME CLI", Order = 8)]
        public string ClientName { get; set; }
        [XL(Name = "UF CLI", Order = 9)]
        public string ClientState { get; set; }
        [XL(Name = "CNPJ", Order = 10)]
        public string CompanyCode { get; set; }
        [XL(Name = "RAZAO", Order = 11)]
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
            EmissionDate = doc.GetElementsByTagName("dhEvento")[0].InnerText;
            Key = doc.GetElementsByTagName("chNFe")[0].InnerText;
            CompanyCode = doc.GetElementsByTagName("CNPJ")[0].InnerText;
            Type = 99;
            Items = new List<IFiscalDocumentItem>();
            Items.Add(new NFeItem());
        }
    }
}

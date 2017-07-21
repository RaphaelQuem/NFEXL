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
    public class NFe : IFiscalDocument
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

        public NFe(XmlDocument doc)
        {

            Key = doc.GetNodeValue("chNFe", "infProt");
            State = doc.GetNodeValue("cUF", "ide");
            Mod = doc.GetNodeValue("mod");
            Type = doc.GetNodeValue("tpNF").ToNumericType<int>();
            DocumentNumber = doc.GetNodeValue("nNF").ToNumericType<uint>();
            EmissionDate = doc.GetNodeValue("dhEmi", "ide");
            CompanyCode = doc.GetNodeValue("CNPJ", "emit");
            CompanyName = doc.GetNodeValue("xNome", "emit");
            ClientCode = doc.GetNodeValue("CPF", "dest");
            ClientName = doc.GetNodeValue("xNome", "dest");
            ClientState = doc.GetNodeValue("UF", "enderDest");
            TotalValue = doc.GetNodeValue("vNF").ToNumericType<double>();
            TotalDiscount = doc.GetNodeValue("vDesc", "ICMSTot").ToNumericType<double>();
            TotalShipping = doc.GetNodeValue("vFrete", "ICMSTot").ToNumericType<double>(); ;

            Items = new List<IFiscalDocumentItem>();

            double sumship = 0;
            double sumdisc = 0;
            XmlNodeList detElements = doc.GetElementsByTagName("det");
            foreach (XmlNode nod in detElements)
            {
                IFiscalDocumentItem item = nod.ToFiscalDocumentItem(ItemType.NfeItemType);
                item.PartialShipping = Math.Round((item.CalcBase / TotalValue)  * TotalShipping, 2);
                item.PartialDiscount = Math.Round((item.CalcBase / TotalValue) * TotalDiscount, 2);

                sumship += item.PartialShipping;
                sumdisc += item.PartialDiscount;

                Items.Add(item);
            }

            double shipleft = TotalShipping - sumship;
            double discleft = TotalDiscount - sumdisc;

            Items[0].PartialShipping += shipleft;
            Items[0].PartialDiscount += discleft;





        }
    }
}

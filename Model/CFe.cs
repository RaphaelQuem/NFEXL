using NFEXL.Attributes;
using NFEXL.Extension;
using NFEXL.Interface;
using System;
using System.Collections.Generic;
using System.Xml;

namespace NFEXL.Model
{
    public class CFe : IFiscalDocument
    {
        [XL(Name = "DATA", Order = 1)]
        public string EmissionDate { get; set; }
        [XL(Name = "CHAVE", Order = 2)]
        public string Key { get; set; }
        [XL(Name = "SERIE", Order = 3)]
        public uint Series { get; set; }
        [XL(Name = "NUMERO", Order = 4)]
        public uint DocumentNumber { get; set; }
        [XL(Name = "MOD", Order = 5)]
        public string Mod { get; set; }
        [XL(Name = "TIPO", Order = 6)]
        public int Type { get; set; }
        [XL(Name = "UF", Order = 7)]
        public string State { get; set; }
        [XL(Name = "CPF", Order = 8)]
        public string ClientCode { get; set; }
        [XL(Name = "NOME CLI", Order = 9)]
        public string ClientName { get; set; }
        [XL(Name = "UF CLI", Order = 10)]
        public string ClientState { get; set; }
        [XL(Name = "CNPJ", Order = 11)]
        public string CompanyCode { get; set; }
        [XL(Name = "RAZAO", Order = 12)]
        public string CompanyName { get; set; }
        [XL(Name = "ITEMS", Order = 99)]
        public List<IFiscalDocumentItem> Items { get; set; }
        [XL(Name = "DESCONTO", Order = 99)]
        public double TotalDiscount { get; set; }
        [XL(Name = "FRETE", Order = 99)]
        public double TotalShipping { get; set; }
        [XL(Name = "TOTAL", Order = 99)]
        public double TotalValue { get; set; }

        public CFe(XmlDocument doc)
        {

            bool canc = false;
            Key = doc.GetElementsByTagName("infCFe")[0].Attributes[0].Value;
            foreach(XmlAttribute att in doc.GetElementsByTagName("infCFe")[0].Attributes)
            {
                if (att.Name.Equals("chCanc"))
                {
                    canc = true;
                    Key = att.Value;
                }
            }

            if (!canc)
            {
                State = doc.GetNodeValue("cUF");
                Mod = doc.GetNodeValue("mod");
                Type = doc.GetNodeValue("tpNF").ToNumericType<int>();
                Series = doc.GetNodeValue("serie").ToNumericType<uint>();
                DocumentNumber = doc.GetNodeValue("nCFe").ToNumericType<uint>();
                EmissionDate = doc.GetNodeValue("dEmi", "ide");
                CompanyCode = doc.GetNodeValue("CNPJ", "emit");
                CompanyName = doc.GetNodeValue("xNome", "emit");
                ClientCode = doc.GetNodeValue("CPF", "dest");
                ClientName = doc.GetNodeValue("xNome", "dest");
                ClientState = doc.GetNodeValue("UF", "enderDest");
                TotalValue = doc.GetNodeValue("vCFe", "total").ToNumericType<double>();
                TotalDiscount = doc.GetNodeValue("vDesc", "total").ToNumericType<double>();
                TotalShipping = doc.GetNodeValue("vFrete", "total").ToNumericType<double>();


                Items = new List<IFiscalDocumentItem>();

                double sumdisc = 0;
                double sumship = 0;
                XmlNodeList detElements = doc.GetElementsByTagName("det");
                foreach (XmlNode nod in detElements)
                {
                    IFiscalDocumentItem item = nod.ToFiscalDocumentItem(ItemType.NfeItemType);
                    item.PartialDiscount = Math.Round(item.TotalValue / (TotalValue - TotalShipping + TotalDiscount) * TotalDiscount, 2);
                    item.PartialShipping = Math.Round(item.TotalValue / (TotalValue - TotalShipping + TotalDiscount) * TotalShipping, 2);

                    sumdisc += item.PartialDiscount;
                    sumship += item.PartialShipping;

                    Items.Add(item);
                }

                double discleft = TotalDiscount - sumdisc;
                double shipleft = TotalShipping - sumship;


                Items[0].PartialDiscount += discleft;
                Items[0].PartialDiscount += shipleft;
            }
            else
            {
                State = doc.GetNodeValue("cUF");
                Mod = doc.GetNodeValue("mod");
                Type = 99;
                DocumentNumber = doc.GetNodeValue("nCFe").ToNumericType<uint>();
                EmissionDate = doc.GetNodeValue("dEmi", "ide");
                CompanyCode = doc.GetNodeValue("CNPJ", "emit");
                CompanyName = doc.GetNodeValue("xNome", "emit");
                ClientCode = doc.GetNodeValue("CPF", "dest");
                ClientName = doc.GetNodeValue("xNome", "dest");
                ClientState = doc.GetNodeValue("UF", "enderDest");
                TotalValue = doc.GetNodeValue("vCFe", "total").ToNumericType<double>();
                TotalDiscount = doc.GetNodeValue("vDesc", "total").ToNumericType<double>();
                TotalShipping = doc.GetNodeValue("vFrete", "total").ToNumericType<double>();
                Items = new List<IFiscalDocumentItem>();
                Items.Add(new NFeItem());

            }


        }
    }
}

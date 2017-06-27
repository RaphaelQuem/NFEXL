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

        [ColumnName(Name = "ITEMS", Order = 99)]
        public List<IFiscalDocumentItem> Items
        {
            get;
            set;
        }
        [ColumnName(Name = "DATA", Order = 1)]
        public string EmissionDate
        {
            get;
            set;
        }
        [ColumnName(Name = "CHAVE", Order = 2)]
        public string Key
        {
            get;
            set;
        }
        [ColumnName(Name = "TIPO", Order = 3)]
        public string Mod
        {
            get;
            set;
        }
        [ColumnName(Name = "UF", Order = 4)]
        public string State
        {
            get;
            set;
        }
        [ColumnName(Name = "CPF", Order = 5)]
        public string ClientCode
        {
            get;
            set;
        }
        [ColumnName(Name = "NOME CLI", Order = 6)]
        public string ClientName
        {
            get;
            set;
        }
        [ColumnName(Name = "UF CLI", Order = 7)]
        public string ClientState
        {
            get;
            set;
        }
        [ColumnName(Name = "CNPJ", Order = 8)]
        public string CompanyCode
        {
            get;
            set;
        }
        [ColumnName(Name = "RAZAO", Order = 9)]
        public string CompanyName { get; set; }
        [ColumnName(Name = "NUMERO", Order = 10)]
        public uint DocumentNumber
        {
            get;
            set;
        }
        [ColumnName(Name = "DESCONTO", Order = 11)]
        public double TotalDiscount
        {
            get;
            set;
        }
        [ColumnName(Name = "FRETE", Order = 12)]
        public double TotalShipping
        {
            get;
            set;
        }
        [ColumnName(Name = "TOTAL", Order = 13)]
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
            }
            



        }
    }
}

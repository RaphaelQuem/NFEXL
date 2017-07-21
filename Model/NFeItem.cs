using NFEXL.Attributes;
using NFEXL.Extension;
using NFEXL.Interface;
using System.Xml;
using System;

namespace NFEXL.Model
{
    public class NFeItem : IFiscalDocumentItem
    {
        [XL(Name = "COD PRODUTO", Order = 1)]
        public string ProductCode { get; set; }
        [XL(Name = "PRODUTO", Order = 2)]
        public string ProductName { get; set; }
        [XL(Name = "NCM", Order = 3)]
        public uint ProductFiscalGroup { get; set; }
        [XL(Name = "CFOP", Order = 4)]
        public int OperationCode { get; set; }
        [XL(Name = "QTD", Order = 5)]
        public double Amount { get; set; }
        [XL(Name = "VALOR UNITARIO", Order = 6)]
        public double UnitValue { get; set; }
        [XL(Name = "VALOR TOTAL", Order = 7)]
        public double TotalValue { get; set; }
        [XL(Name = "CST", Order = 8)]
        public int ProductOriginCode { get; set; }
        [XL(Name = "ICMS%", Order = 9)]
        public double Tax1Perc { get; set; }
        [XL(Name = "ICMS", Order = 10)]
        public double Tax1 { get; set; }
        [XL(Name = "PIS%", Order = 11)]
        public double Tax2Perc { get; set; }
        [XL(Name = "PIS", Order = 12)]
        public double Tax2 { get; set; }
        [XL(Name = "COFINS%", Order = 13)]
        public double Tax3Perc { get; set; }
        [XL(Name = "COFINS", Order = 14)]
        public double Tax3 { get; set; }
        [XL(Name = "FRETE", Order = 15)]
        public double PartialShipping { get; set; }
        [XL(Name = "DESCONTO", Order = 16)]
        public double PartialDiscount { get; set; }
        [XL(Name = "DESCONTO", Order = 99)]
        public double CalcBase { get; set; }

        public NFeItem(XmlNode nod)
        {
            ProductCode = nod.GetNodeByPath("prod/cProd");
            ProductName = nod.GetNodeByPath("prod/xProd");
            PartialDiscount = nod.GetNodeByPath("prod/vDesc").ToNumericType<double>();
            ProductFiscalGroup = nod.GetNodeByPath("prod/NCM").ToNumericType<uint>();
            OperationCode = nod.GetNodeByPath("prod/CFOP").ToNumericType<int>();
            Amount = nod.GetNodeByPath("prod/qCom").ToNumericType<double>();
            UnitValue = nod.GetNodeByPath("prod/vUnCom").ToNumericType<double>();
            TotalValue = nod.GetNodeByPath("prod/vProd").ToNumericType<double>();
            CalcBase = nod.GetNodeByPath("imposto/ICMS/ICMS00/vBC").ToNumericType<double>();

            ProductOriginCode = nod.GetNodeByPath("prod/CST").ToNumericType<int>();
            Tax1Perc = nod.GetNodeByPath("imposto/ICMS/ICMS00/pICMS").ToNumericType<double>();
            Tax1 = nod.GetNodeByPath("imposto/ICMS/ICMS00/vICMS").ToNumericType<double>();
            Tax2Perc = nod.GetNodeByPath("imposto/PIS/PISAliq/pPIS").ToNumericType<double>();
            Tax2 = nod.GetNodeByPath("imposto/PIS/PISAliq/vPIS").ToNumericType<double>();
            Tax3Perc = nod.GetNodeByPath("imposto/COFINS/COFINSAliq/pCOFINS").ToNumericType<double>();
            Tax3 = nod.GetNodeByPath("imposto/COFINS/COFINSAliq/vCOFINS").ToNumericType<double>();
        }
        public NFeItem()
        {
        }
    }
}

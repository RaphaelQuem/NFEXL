using NFEXL.Attributes;
using NFEXL.Extension;
using System.Xml;

namespace NFEXL.Model
{
    public class CFeItem : IFiscalDocumentItem
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
        public CFeItem(XmlNode nod)
        {
            ProductCode = nod.GetNodeValue("prod/cProd");
            ProductName = nod.GetNodeValue("prod/xProd");
            ProductFiscalGroup = nod.GetNodeValue("prod/NCM").ToNumericType<uint>();
            OperationCode = nod.GetNodeValue("prod/CFOP").ToNumericType<int>();
            Amount = nod.GetNodeValue("prod/qCom").ToNumericType<double>();
            UnitValue = nod.GetNodeValue("prod/vUnCom").ToNumericType<double>();
            TotalValue = nod.GetNodeValue("prod/vProd").ToNumericType<double>();
            ProductOriginCode = nod.GetNodeValue("prod/CST").ToNumericType<int>();
            Tax1Perc = nod.GetNodeValue("imposto/ICMS/ICMS00/pICMS").ToNumericType<double>();
            Tax1 = nod.GetNodeValue("imposto/ICMS/ICMS00/vICMS").ToNumericType<double>();
            Tax2Perc = nod.GetNodeValue("imposto/PIS/PISAliq/pPIS").ToNumericType<double>();
            Tax2 = nod.GetNodeValue("imposto/PIS/PISAliq/vPIS").ToNumericType<double>();
            Tax3Perc = nod.GetNodeValue("imposto/COFINS/COFINSAliq/pCOFINS").ToNumericType<double>();
            Tax3 = nod.GetNodeValue("imposto/COFINS/COFINSAliq/vCOFINS").ToNumericType<double>();
        }
    }
}

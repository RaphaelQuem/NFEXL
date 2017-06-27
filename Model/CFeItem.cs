using NFEXL.Attributes;
using NFEXL.Extension;
using System.Xml;

namespace NFEXL.Model
{
    public class CFeItem : IFiscalDocumentItem
    {
        [ColumnName(Name = "A", Order = 1)]
        public string ProductCode { get; set; }
        [ColumnName(Name = "A", Order = 2)]
        public string ProductName { get; set; }
        [ColumnName(Name = "A", Order = 3)]
        public uint ProductFiscalGroup { get; set; }
        [ColumnName(Name = "A", Order = 4)]
        public int OperationCode { get; set; }
        [ColumnName(Name = "A", Order = 5)]
        public double Amount { get; set; }
        [ColumnName(Name = "A", Order = 6)]
        public double UnitValue { get; set; }
        [ColumnName(Name = "A", Order = 7)]
        public double TotalValue { get; set; }
        [ColumnName(Name = "A", Order = 8)]
        public int ProductOriginCode { get; set; }
        [ColumnName(Name = "A", Order = 9)]
        public double Tax1Perc { get; set; }
        [ColumnName(Name = "A", Order = 10)]
        public double Tax1 { get; set; }
        [ColumnName(Name = "A", Order = 11)]
        public double Tax2Perc { get; set; }
        [ColumnName(Name = "A", Order = 12)]
        public double Tax2 { get; set; }
        [ColumnName(Name = "A", Order = 13)]
        public double Tax3Perc { get; set; }
        [ColumnName(Name = "A", Order = 14)]
        public double Tax3 { get; set; }
        [ColumnName(Name = "A", Order = 15)]
        public double PartialShipping { get; set; }
        [ColumnName(Name = "A", Order = 16)]
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

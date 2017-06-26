using System.Xml;

namespace NFEXL.Model
{
    public class CFeItem : IFiscalDocumentItem
    {
        public string Key { get; set; }
        public string State { get; set; }
        public string Mod { get; set; }
        public uint DocumentNumber { get; set; }
        public string EmissionDate { get; set; }
        public string CompanyCode { get; set; }
        public string ClientCode { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public uint ProductFiscalGroup { get; set; }
        public int OperationCode { get; set; }
        public double Amount { get; set; }
        public double UnitValue { get; set; }
        public double TotalValue { get; set; }
        public int ProductOriginCode { get; set; }
        public double Tax1Perc { get; set; }
        public double Tax1 { get; set; }
        public double Tax2Perc { get; set; }
        public double Tax2 { get; set; }
        public double Tax3Perc { get; set; }
        public double Tax3 { get; set; }
        public CFeItem(XmlNode doc)
        {
          
        }
    }
}

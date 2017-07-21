namespace NFEXL.Interface
{
    public interface IFiscalDocumentItem
    {
        string ProductCode { get; set; }
        string ProductName  { get; set; }
        uint ProductFiscalGroup { get; set; }
        int OperationCode { get; set; }
        double Amount{ get; set; }
        double UnitValue { get; set; }
        double TotalValue { get; set; }
        double CalcBase { get; set; }
        int ProductOriginCode { get; set; }
        double Tax1Perc{ get; set; }
        double Tax1 { get; set; }
        double Tax2Perc { get; set; }
        double Tax2 { get; set; }
        double Tax3Perc { get; set; }
        double Tax3 { get; set; }
        double PartialShipping { get; set; }
        double PartialDiscount { get; set; }

    }
}

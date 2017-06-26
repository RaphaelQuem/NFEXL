using System.Collections.Generic;

namespace NFEXL.Model
{
    public interface IFiscalDocument
    {
        string Key { get; set; }
        string State { get; set; }
        string Mod { get; set; }
        uint DocumentNumber { get; set; }
        string EmissionDate { get; set; }
        string CompanyCode { get; set; }
        string CompanyName { get; set; }
        string ClientCode { get; set; }
        string ClientName { get; set; }
        List<IFiscalDocumentItem> Items { get; set; }
        double TotalDiscount { get; set; }
        double TotalShipping { get; set; }
        double TotalValue { get; set; }
    }
}

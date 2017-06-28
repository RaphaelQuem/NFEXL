using NFEXL.Attributes;
using NFEXL.Extension;
using NFEXL.Interface;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NFEXL.Controller
{
    public class NFEXLController
    {
        public void ExportXML(string outputpath, string inputpath)
        {
            FileInfo file = new FileInfo(outputpath);

            string dir = inputpath;


            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Exportação");
                ExcelWorksheet logsheet = package.Workbook.Worksheets.Add("Log");


                int rowNumber = 2;
                int logrownumber = 1;
                foreach (string ped in Directory.EnumerateFiles(dir))
                {
                    XmlDocument xml = new XmlDocument();
                    xml.Load(ped);
                    try
                    {
                        WriteDocXL(xml.ToFiscalDocument(), worksheet, ref rowNumber, logsheet, ref logrownumber);
                    }
                    catch
                    {

                    }
                    rowNumber++;
                }

                package.Save();

            }

        }
        private void WriteDocXL(IFiscalDocument doc, ExcelWorksheet sheet, ref int rowNumber, ExcelWorksheet logsheet, ref int logrownumber)
        {
            for (int fis = 0; fis < doc.Items.Count; fis++)
            {
                IFiscalDocumentItem item = doc.Items[fis];
                int add = 0;
                for (int i = 1; i <= doc.GetType().GetProperties().Where(prp => prp.GetXLAttribute().Order < 99).Count(); i++)
                {
                    try
                    {
                        PropertyInfo prop = doc.GetType().GetPropertyByOrder(i);
                        XLAttribute att = prop.GetXLAttribute();

                        if (rowNumber.Equals(2))
                        {
                            sheet.Cells[rowNumber - 1, i].Value = att.Name;
                        }
                        sheet.Cells[rowNumber, i].Value = doc.GetType().GetProperties()[i - 1].GetValue(doc);
                    }
                    catch (Exception ex)
                    {
                        logsheet.Cells[logrownumber, 1].Value = string.Format("Erro no XML da nota {0}: {1}", doc.DocumentNumber, ex.Message);
                        logrownumber++;
                    };
                    add++;
                }
                for (int i = 1; i <= item.GetType().GetProperties().Count(); i++)
                {
                    try
                    {
                        PropertyInfo itprop = item.GetType().GetPropertyByOrder(i);
                        XLAttribute itatt = itprop.GetXLAttribute();

                        if (rowNumber.Equals(2))
                        {
                            sheet.Cells[rowNumber - 1, i + add].Value = itatt.Name;
                        }
                        sheet.Cells[rowNumber, i + add].Value = item.GetType().GetProperties()[i - 1].GetValue(item);
                    }
                    catch (Exception ex)
                    {
                        logsheet.Cells[logrownumber, 1].Value = string.Format("Erro no XML da nota {0} no produto {1}: {2}", doc.DocumentNumber, item.ProductCode, ex.Message);
                        logrownumber++;
                    };
                }
                if (!fis.Equals(doc.Items.Count - 1))
                    rowNumber++;
            }
        }
    }
}

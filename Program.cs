using NFEXL.Attributes;
using NFEXL.Extension;
using NFEXL.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            FileInfo file = new FileInfo("C:\\NFE-APP\\RESULTADO-" + DateTime.Today.ToShortDateString().Replace("/", "-") + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + ".XLSX");

            Console.WriteLine("Entre com o caminho:");

            string dir = Console.ReadLine().Replace("\\", "\\\\");


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
                        WriteDocXL(xml.ToFiscalDocument(), worksheet,ref rowNumber, logsheet,ref logrownumber);
                    }
                    catch
                    {

                    }
                    rowNumber++;
                }

                package.Save();

            }
        }
        public static void WriteDocXL(IFiscalDocument doc, ExcelWorksheet sheet, ref int rowNumber, ExcelWorksheet logsheet,ref int logrownumber)
        {
            for (int fis = 0; fis < doc.Items.Count;fis++)
            {
                IFiscalDocumentItem item = doc.Items[fis];
                int add=0;
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
                            sheet.Cells[rowNumber - 1, i+add].Value = itatt.Name;
                        }
                        sheet.Cells[rowNumber, i+add].Value = item.GetType().GetProperties()[i - 1].GetValue(item);
                    }
                    catch (Exception ex)
                    {
                        logsheet.Cells[logrownumber, 1].Value = string.Format("Erro no XML da nota {0} no produto {1}: {2}", doc.DocumentNumber, item.ProductCode, ex.Message);
                        logrownumber++;
                    };
                }
                if(!fis.Equals(doc.Items.Count -1))
                    rowNumber++;
            }
        }

    /*
     private void ReturnCFE(string ped)
         {
         XmlDocument doc = new XmlDocument();
         doc.Load("");

         XmlReader rdr = XmlReader.Create(ped);
         XmlNode node = doc.ReadNode(rdr);
         node = doc.GetElementsByTagName("infProt")[0];
         if (node != null)
         {
             foreach (XmlNode son in node.ChildNodes)
             {
                 if (son.Name.Equals("chNFe"))
                     chave = son.InnerText;
             }
         }

         if (chave.Equals(""))
         {
             node = doc.GetElementsByTagName("infCFe")[0].Attributes[0];
             chave = node.Value;

         }


         node = doc.GetElementsByTagName("ide")[0];
         foreach (XmlNode son in node.ChildNodes)
         {
             if (son.Name.Equals("dhEmi") || son.Name.Equals("dEmi"))
                 data = son.InnerText.Substring(0, (son.InnerText.Length > 10 ? 10 : son.InnerText.Length));

         }

         node = doc.GetElementsByTagName("emit")[0];
         if (node != null && node.ChildNodes != null)
         {
             foreach (XmlNode son in node.ChildNodes)
             {
                 if (son.Name.Equals("CNPJ"))
                     cnpj = son.InnerText;
                 if (son.Name.Equals("xNome"))
                     razao = son.InnerText;
             }
         }



         node = doc.GetElementsByTagName("dest")[0];
         if (node != null && node.ChildNodes != null)
         {
             foreach (XmlNode son in node.ChildNodes)
             {
                 if (son.Name.Equals("CPF"))
                     cpf = son.InnerText;
                 if (son.Name.Equals("xNome"))
                     cli = son.InnerText;
             }
         }

         node = doc.GetElementsByTagName("enderDest")[0];
         if (node != null && node.ChildNodes != null)
         {
             foreach (XmlNode son in node.ChildNodes)
             {
                 if (son.Name.Equals("UF"))
                     uf = son.InnerText;

             }
         }
         node = doc.GetElementsByTagName("vNF")[0];
         if (node != null)
             vnf = node.InnerText;

         if (vnf.Equals(""))
         {
             node = doc.GetElementsByTagName("vCFe")[0];
             if (node != null)
                 vnf = node.InnerText;
         }

         node = doc.GetElementsByTagName("ICMSTot")[0];
         if (node != null && node.ChildNodes != null)
         {
             foreach (XmlNode son in node.ChildNodes)
             {
                 if (son.Name.Equals("vProd"))
                     basecalc = son.InnerText;

             }
         }


         node = doc.GetElementsByTagName("natOp")[0];
         if (node != null)
             natop = node.InnerText;

         node = doc.GetElementsByTagName("tpNF")[0];
         if (node != null)
             entsai = node.InnerText;


     }*/
}
}

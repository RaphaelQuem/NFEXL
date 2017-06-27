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


            FileInfo file = new FileInfo("C:\\NFE-APP\\RESULTADO-" + DateTime.Today.ToShortDateString().Replace("/", "-") + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + ".XLS");

            Console.WriteLine("Entre com o caminho:");

            string dir = Console.ReadLine().Replace("\\", "\\\\");


            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Exportação");
                ExcelWorksheet logsheet = package.Workbook.Worksheets.Add("Log");


                int rowNumber = 2;
                foreach (string ped in Directory.EnumerateFiles(dir))
                {

                    XmlDocument xml = new XmlDocument();
                    xml.Load(ped);
                    try
                    {
                        IFiscalDocument doc = xml.ToFiscalDocument();
                        for (int i = 0; i < doc.GetType().GetProperties().Count(); i++)
                        {
                            try
                            {
                                PropertyInfo prop = doc.GetType().GetProperties().Where(prp => ((ColumnNameAttribute)prp.GetCustomAttributes(true)[0]).Order.Equals(i)).FirstOrDefault();
                                ColumnNameAttribute att = ((ColumnNameAttribute)prop.GetCustomAttributes(true)[0]);

                                if (rowNumber.Equals(2))
                                {
                                    worksheet.Cells[rowNumber - 1, i].Value = att.Name;
                                }
                                worksheet.Cells[rowNumber, i].Value = doc.GetType().GetProperties()[i].GetValue(doc);

                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                    catch
                    {

                    }
                    rowNumber++;
                }

                package.Save();

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

using NFEXL.Extension;
using NFEXL.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ConsoleApplication1
{
    class Program
    {
        static string cnpj = "";
        static string razao = "";
        static string data = "";
        static string chave = "";
        static string cpf = "";
        static string cli = "";
        static string uf = "";
        static string vnf = "";
        static string natop = "";
        static string entsai = "";
        static string basecalc = "";
        static void Main(string[] args)
        {


            FileInfo file = new FileInfo("C:\\NFE-APP\\RESULTADO-" + DateTime.Today.ToShortDateString().Replace("/", "-") + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + ".XLS");

            Console.WriteLine("Entre com o caminho:");

            string dir = Console.ReadLine().Replace("\\", "\\\\");


            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Exportação");

                worksheet.Cells[1, 1].Value = "CNPJ";
                worksheet.Cells[1, 2].Value = "RAZAO";
                worksheet.Cells[1, 3].Value = "DATA";
                worksheet.Cells[1, 4].Value = "CHAVE";
                worksheet.Cells[1, 5].Value = "CPF";
                worksheet.Cells[1, 6].Value = "NOME";
                worksheet.Cells[1, 7].Value = "UF";
                worksheet.Cells[1, 8].Value = "BC";
                worksheet.Cells[1, 9].Value = "TOTAL";
                worksheet.Cells[1, 10].Value = "NAT OP";
                worksheet.Cells[1, 11].Value = "TIPO";



                int rowNumber = 2;
                foreach (string ped in Directory.EnumerateFiles(dir))
                {
                    try
                    {
                        XmlDocument xml = new XmlDocument();
                        xml.Load(ped);

                        IFiscalDocument doc = xml.ToFiscalDocument();   

                    }
                    catch
                    {

                    }
                    worksheet.Cells[rowNumber, 1].Value = cnpj;
                    worksheet.Cells[rowNumber, 2].Value = razao;
                    worksheet.Cells[rowNumber, 3].Value = data;
                    worksheet.Cells[rowNumber, 4].Value = chave;
                    worksheet.Cells[rowNumber, 5].Value = cpf;
                    worksheet.Cells[rowNumber, 6].Value = cli;
                    worksheet.Cells[rowNumber, 7].Value = uf;
                    worksheet.Cells[rowNumber, 8].Value = basecalc;
                    worksheet.Cells[rowNumber, 9].Value = vnf;
                    worksheet.Cells[rowNumber, 10].Value = natop;
                    worksheet.Cells[rowNumber, 11].Value = entsai;


                    rowNumber++;
                }

                package.Save();

            }
        }
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


        }
    }
}

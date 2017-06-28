using NFEXL.Attributes;
using NFEXL.Extension;
using NFEXL.Interface;
using NFEXL.Controller;
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

            NFEXLController controller = new NFEXLController();

            while (true)
            {
                Console.WriteLine("Entre com o caminho:");

                string dir = Console.ReadLine().Replace("\\", "\\\\");

                controller.ExportXML("C:\\NFE-APP\\RESULTADO-" + DateTime.Today.ToShortDateString().Replace("/", "-") + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + ".XLSX",dir);

            }
        }
    }
}


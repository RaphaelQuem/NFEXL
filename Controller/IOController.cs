using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NFEXL.Controller
{
    public class IOController
    {
        public List<string> GetPaths(string io)
        {

            RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE", true);

            key.CreateSubKey("NFEXL");
            key = key.OpenSubKey("NFEXL", true);

            return (key.GetValue(io)==null? new List<string>():key.GetValue(io).ToString().Replace("\r","").Split('\n').ToList());

        }
        public void PersistPaths(string input, string output)
        {
            AppendTextToRegistry("INPUT", input);
            AppendTextToRegistry("OUTPUT", output);
        }

        private void AppendTextToFile(string path, string newline)
        {
            string text = "";
            if (File.Exists(path))
            {
                using (StreamReader rdr = new StreamReader(path))
                {
                    text = rdr.ReadToEnd();
                }
                if (text.Split('\n').Count() >= 10)
                    text = text.Replace(text.Substring(0, text.IndexOf('\n')), "").Trim();
                if (!text.Contains(newline))
                {
                    using (StreamWriter wtr = new StreamWriter(path))
                    {
                        wtr.Write(text + Environment.NewLine + newline);
                    }
                }
            }
            else
            {
                using (StreamWriter wtr = new StreamWriter(path))
                {
                    wtr.WriteLine(newline);
                }

            }
        }
        public void AppendTextToRegistry(string id,string newline)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE",true);

            key.CreateSubKey("NFEXL");
            key = key.OpenSubKey("NFEXL", true);

            string text = (key.GetValue(id)??"").ToString();
            if (text.Split('\n').Count() >= 10)
                text = text.Replace(text.Substring(0, text.IndexOf('\n')), "").Trim();
            if (!text.Contains(newline))
            {

                text += Environment.NewLine + newline;
            }

            key.SetValue(id, text);

        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NFEXL.Controller
{
    public class IOController
    {
        private const string INPUTPATH = "C:\\NFEXL.in";
        private const string OUTPUTPATH = "C:\\NFEXL.out";
        public List<string> GetOutputPaths()
        {
            List<string> ret = new List<string>();
            if (File.Exists(OUTPUTPATH))
            {
                StreamReader rdr = new StreamReader(OUTPUTPATH);
                while (!rdr.EndOfStream)
                {
                    ret.Add(rdr.ReadLine().Trim());
                }
            }
            return ret;
        }
        public List<string> GetInputPaths()
        {
            List<string> ret = new List<string>();
            if (File.Exists(INPUTPATH))
            {
                StreamReader rdr = new StreamReader(INPUTPATH);
                while (!rdr.EndOfStream)
                {
                    ret.Add(rdr.ReadLine().Trim());
                }
            }
            return ret;
        }
        public void PersistPaths(string input, string output)
        {
            AppendTextToFile(INPUTPATH, input);
            AppendTextToFile(OUTPUTPATH, output);
        }
        private void AppendTextToFile(string path,string newline)
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
                using (StreamWriter wtr = new StreamWriter(path ))
                {
                    wtr.WriteLine(newline);
                }

            }
        }
    }
}

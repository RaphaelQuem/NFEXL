using System;
using System.Collections.Generic;
using System.IO;


namespace NFEXL.Controller
{
    public class IOController
    {
        public List<string> GetOutputPaths()
        {
            List<string> ret = new List<string>();
            string path = "C:\\NFEXL.out";
            if (File.Exists(path))
            {
                StreamReader rdr = new StreamReader(path);
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
            string path = "C:\\NFEXL.in";
            if (File.Exists(path))
            {
                StreamReader rdr = new StreamReader(path);
                while (!rdr.EndOfStream)
                {
                    ret.Add(rdr.ReadLine().Trim());
                }
            }
            return ret;
        }
    }
}

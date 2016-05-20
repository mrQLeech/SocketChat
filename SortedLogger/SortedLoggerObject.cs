using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortedLogger
{
    public class SortedLoggerObject: ILogger
    {
        private string filePath;
       
        public SortedLoggerObject(string logFilePath)
        {
            filePath = logFilePath;
            PrepareFile(filePath);
        }
 
        public void LogRecord(string line)
        {
            
            List<string> lines = File.ReadAllLines(filePath).ToList<string>();
            lines.Add(line);
            lines.Sort();
            File.WriteAllLines(filePath, lines);
        }

        private void PrepareFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                var fl = File.Create(filePath);
                fl.Close();
                return;
            }

            File.WriteAllLines(filePath, new List<string>());
        }

        public string GetLog()
        {
            var res = new StringBuilder();
            var lines = File.ReadAllLines(filePath);
            
            foreach(var line in lines)
            {
                res.AppendLine(line);
            }

            return res.ToString();
        }
    }
}

using System;]
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortedLogger
{
    class SortedLoggerObject
    {
        private string filePath;
        public string LogText { get; private set;}

        public SortedLoggerObject(string logFilePath)
        {
            filePath = logFilePath;
        }

        private void Log(string line)
        {
            List<string> lines = File.ReadAllLines(filePath).ToList<string>();
            lines.Add(line);
            lines.Sort();
            File.WriteAllLines(filePath, lines);
        }



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortedLogger
{
    public interface ILogger
    {
        void LogRecord(string record);
        string GetLog();

    }
}

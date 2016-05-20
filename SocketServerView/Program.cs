using SocketServerController;
using SortedLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServerView
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var logger = new SortedLoggerObject(path + "\\socket_chat_log.txt");
            var s = new SocketServerProcessor(logger);
        }
    }
}

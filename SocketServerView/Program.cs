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
            var logger = new SortedLoggerObject("socket_chat_log.txt");
            var s = new SocketServerProcessor(logger);
        }
    }
}

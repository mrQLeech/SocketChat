using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServerController
{
    interface ILoggerAction
    {
        void LogRec(string record);
    }
}

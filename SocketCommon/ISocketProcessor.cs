using System;


namespace SocketCommon
{
    public interface ISocketProcessor
    {
        void SendMessage(string message);

        void CreateConnection();
        void RequestMessageLog();
        void CloseConnection();
        string GetClientId();
        event EventHandler MessageRecieved;
    }
}

using System;


namespace SocketClientController
{
    public interface IClientProcessor
    {
        void SendMessage(string message);
        string GetMessagesLog();
        void CloseConnection();
        string GetClientId();
        event EventHandler MessageRecieved;
    }
}

using System;
using SocketClientController;
using System.Windows.Forms;

namespace SocketClientView
{
    public partial class ChatForm : Form
    {
        private ISocketProcessor connectionProcessor;

        public ChatForm(ISocketProcessor processor)
        {
            InitializeComponent();
            connectionProcessor = processor;
            this.Text = processor.GetClientId();

            connectionProcessor.MessageRecieved += OnMessageRecieved;

            connectionProcessor.CreateConnection();
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        private void messageText_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendMessage();
            }
        }

        private void SendMessage()
        {
            if (!string.IsNullOrEmpty(messageText.Text) 
                 && !string.IsNullOrWhiteSpace(messageText.Text))
            {
                connectionProcessor.SendMessage(messageText.Text);
                messageText.Text = "";
            }            
        }

        private void OnMessageRecieved(Object sender, EventArgs arg)
        {
            var mess = ((RecievedMessageEventArgs)arg).Message;
            if (mess.Type == SocketCommon.MessageType.MESSAGE)
            {
                chatText.AppendText(mess.SenderName + ": " + mess.Text);
            }
            
        }

        
    }
}
